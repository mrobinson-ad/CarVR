using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
public class UnitTests
{

    private Transform carAnchor;
    private TextMeshProUGUI year;
    private InteractionController controller;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("CarShowroom", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);
        carAnchor = GameObject.Find("CarAnchor").transform;
    }

    [TearDown]
    public void TearDown()
    {
        SceneManager.UnloadSceneAsync("CarShowroom");
    }

    [UnityTest]
    public IEnumerator TestSpawningCars()
    {
        var hondaButton = GameObject.Find("Honda Civic button");
        Assert.IsNotNull(hondaButton, "Car button was not created");
        controller = GameObject.Find("XR Origin").GetComponent<InteractionController>();

        Button buttonComponent = hondaButton.GetComponent<Button>();
        buttonComponent.onClick.Invoke();

        yield return null;

        Assert.AreEqual(carAnchor.childCount, 1, "Car was not spawned");
        var spawnedCar = carAnchor.GetChild(0).gameObject;
        Assert.AreEqual(spawnedCar.name, "FK8(Clone)", "Spawned car has incorrect name");
        yield return new WaitForSeconds(1);
        var doors = spawnedCar.GetComponentsInChildren<Door>();
        Assert.IsNotEmpty(doors, "No doors found on the spawned car.");

        var frontLeftDoor = System.Array.Find(doors, door => door.doorType == DoorType.FrontLeft);
        Assert.IsNotNull(frontLeftDoor, "Front left door not found on the car.");

        Assert.IsFalse(frontLeftDoor.isOpen, "Front left door should initially be closed.");

        controller.ToggleDoor(DoorType.FrontLeft);
        yield return new WaitForSeconds(1.2f);

        Assert.IsTrue(frontLeftDoor.isOpen, "Front left door should be open after toggling.");
        

        controller.ToggleDoor(DoorType.FrontLeft);
        yield return new WaitForSeconds(1.2f);

        Assert.IsFalse(frontLeftDoor.isOpen, "Front left door should be closed after toggling again.");

        yield return null;


        year = GameObject.Find("Year").GetComponent<TextMeshProUGUI>();
        var wristUI = GameObject.Find("WristGrid").transform;

        Assert.AreEqual(year.text, "Year: 2018", "Car data has not been set correctly");
        Assert.IsTrue(wristUI.childCount > 0, "Color changer is not set properly");

        var volvoButton = GameObject.Find("Volvo S90 button");
        buttonComponent = volvoButton.GetComponent<Button>();
        buttonComponent.onClick.Invoke();

        yield return null;

        spawnedCar = carAnchor.GetChild(0).gameObject;
        Assert.AreEqual(spawnedCar.name, "Volvo S90(Clone)", "Spawned car has incorrect name");

        yield return new WaitForSeconds(1);
        Assert.AreEqual(year.text, "Year: 2019", "Car data has not been set correctly");
        Assert.IsTrue(wristUI.childCount > 0, "Color changer is not set properly");
    }

    [UnityTest]
    public IEnumerator TestInternetCheck()
    {
        var internetScript = GameObject.Find("InternetCheck").GetComponent<InternetCheck>();

        yield return new WaitForSeconds(1.5f);

        Assert.IsFalse(internetScript.displayError.activeInHierarchy, "API not reached and error is displayed");
    }
}
