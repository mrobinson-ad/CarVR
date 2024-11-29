using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : MonoBehaviour, ICarInteraction 
{
    public AudioClip horn;
    public AudioClip engine;
    public AudioSource carAudio;



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
        
    }


}
