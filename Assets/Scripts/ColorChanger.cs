using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private Transform gridLayout;
    [SerializeField]
    private GameObject colorButtonPrefab;

    private void OnEnable()
    {
        CatalogueManager.Instance.OnCarSpawned += SetColors;
    }

    private void OnDisable()
    {
        if (CatalogueManager.Instance != null)
        {
            CatalogueManager.Instance.OnCarSpawned -= SetColors;
        }
    }


    private void SetColors(Car_SO car)
    {
        Debug.Log("setColors");
        Debug.Log(car.carColors.Count);
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }
        int i = 1;
        foreach (Color color in car.carColors)
        {
            GameObject newButton = Instantiate(colorButtonPrefab, gridLayout);

            newButton.name = "Color " + i;

            newButton.GetComponent<Image>().color = color;

            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(() => ChangeColor(car, color));
            newButton.transform.SetSiblingIndex(1);
            i++;
        }
    }

    private void ChangeColor(Car_SO car, Color color)
    {
        Debug.Log("ColorChange");
        car.carPaint.color = color;
    }
}
