
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Scriptable Objects/Car")]
public class Car_SO : ScriptableObject
{

    public string carName;
    public Sprite carSprite;

    public GameObject carPrefab;
    public Material carPaint;
    public List<Color> carColors;
    public Vector3 anchorPos;
    public Vector3 anchorRotation;
}
