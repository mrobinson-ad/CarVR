using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;


public class CatalogueManager : MonoBehaviour
{
    public static CatalogueManager Instance { get; private set; }

    [SerializeField]
    private List<Car_SO> cars = new();

    [SerializeField]
    private Transform gridLayout;
    [SerializeField]
    private GameObject carButtonPrefab;

    [SerializeField]
    private Transform carAnchor;

    [SerializeField]
    private CarSpecs carSpecs;

    [SerializeField]
    private GameObject specsUI;

    [SerializeField]
    private TextMeshProUGUI model, year, dimensions, horsepower, weight, fuelType, fuelConsumption, fuelCapacity;  


    public event Action<Car_SO> OnCarSpawned;
    private string apiUrl = "https://virtualhome.hopto.org/car/getcar/";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        AddCarButtons();
    }

    private void AddCarButtons()
    {
        foreach (Car_SO car in cars)
        {
            GameObject newButton = Instantiate(carButtonPrefab, gridLayout);

            newButton.name = car.carName + " button";

            newButton.GetComponent<Image>().sprite = car.carSprite;

            Button button = newButton.GetComponent<Button>();
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = car.carName;
            button.onClick.AddListener(() => SpawnCar(car));
            newButton.transform.SetSiblingIndex(1);
        }
    }

    private void SpawnCar(Car_SO car)
    {
        foreach (Transform child in carAnchor)
        {
            Destroy(child.gameObject);
        }
        carAnchor.position = car.anchorPos;
        Instantiate(car.carPrefab, carAnchor.position, carAnchor.rotation, carAnchor);
        OnCarSpawned?.Invoke(car);
        StartCoroutine(GetCarSpecs(car));
    }

    private IEnumerator GetCarSpecs(Car_SO car)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl + car.carName.Replace(" ", "_")))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching car data: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                try

                {
                    carSpecs = JsonConvert.DeserializeObject<CarSpecs>(jsonResponse);
                    SetSpecs(carSpecs);
                    if (!specsUI.activeInHierarchy)
                    {
                        specsUI.SetActive(true);
                    }
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Failed to parse JSON: " + ex.Message);
                }
            }
        }
    }

    private void SetSpecs(CarSpecs specs)
    {
        model.text = "Model: " + specs.carName.Replace("_", " ");
        year.text = "Year: " + specs.carYear;
        dimensions.text = "Dimensions: " + specs.carDimensions;
        weight.text = "Weight: " + specs.carWeight + "kg";
        horsepower.text = "Horsepower: " + specs.carHorsepower;
        fuelType.text = "Fuel type: " + specs.carFuelType;
        fuelCapacity.text = "Fuel capacity: " + specs.carFuelCapacity;
        fuelConsumption.text = "Fuel consumption: " + specs.carConsumption;
    }
}
