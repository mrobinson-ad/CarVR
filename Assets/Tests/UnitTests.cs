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

        Button buttonComponent = hondaButton.GetComponent<Button>();
        buttonComponent.onClick.Invoke();

        yield return null;

        Assert.AreEqual(carAnchor.childCount, 1, "Car was not spawned");
        var spawnedCar = carAnchor.GetChild(0).gameObject;
        Assert.AreEqual(spawnedCar.name, "FK8(Clone)", "Spawned car has incorrect name");

        yield return new WaitForSeconds(1);
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
}
