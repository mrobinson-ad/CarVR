using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corvette : MonoBehaviour, ICarInteraction 
{
    public Dictionary<DoorType, Door> doors = new Dictionary<DoorType, Door>();
    public AudioClip horn;
    public AudioClip engine;
    public AudioSource carAudio;

    private void Awake()
    {
        foreach (Door door in GetComponentsInChildren<Door>())
        {
            doors.Add(door.doorType, door);
        }
    }

    public void StartEngine()
    {
        carAudio.PlayOneShot(engine, 0.5f);
    }

    public void HonkHorn()
    {
        carAudio.PlayOneShot(horn, 0.7f);
    }

    public void ToggleDoor(DoorType doorType)
    {
        if (doors.TryGetValue(doorType, out Door door))
        {
            StartCoroutine(ToggleDoorCoroutine(door));
        }
    }

     private IEnumerator ToggleDoorCoroutine(Door door)
    {
        float targetRotation = door.isOpen ? door.closedY : door.openY;
        float startRotation = door.transform.localEulerAngles.z;
        float elapsedTime = 0f;
        const float duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.LerpAngle(startRotation, targetRotation, elapsedTime / duration);
            door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, door.transform.localEulerAngles.y, angle );
            yield return null;
        }

        door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, door.transform.localEulerAngles.y, targetRotation);
        door.isOpen = !door.isOpen;
    }

}