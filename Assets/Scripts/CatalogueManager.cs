using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    }
}
