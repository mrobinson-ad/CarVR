using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private ICarInteraction currentCar;

    [SerializeField]
    private GameObject carAnchor;
    private void Start()
    {
        CatalogueManager.Instance.OnCarSpawned += SetCar;
    }

    private void OnDisable()
    {
        if (CatalogueManager.Instance != null)
        {
            CatalogueManager.Instance.OnCarSpawned -= SetCar;
        }
    }
    public void SetCar(Car_SO car)
    {
        currentCar = carAnchor.GetComponentInChildren<ICarInteraction>();
        if (currentCar == null)
        {
            Debug.LogWarning("The selected object does not implement ICarInteraction.");
        }
    }

    public void ToggleDoor(DoorType doorType)
    {
        currentCar?.ToggleDoor(doorType);
    }
    public void OnHonkHorn()
    {
        currentCar?.HonkHorn();
    }
}

