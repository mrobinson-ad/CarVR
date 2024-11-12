using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class CarSpecs
{
    public int Car_ID;

    [JsonProperty("Name")]
    public string carName;

    [JsonProperty("Year")]
    public string carYear;

    [JsonProperty("Dimensions")]
    public string carDimensions;

    [JsonProperty("Horsepower")]
    public string carHorsepower;

    [JsonProperty("Weight")]
    public string carWeight;

    [JsonProperty("Fuel_type")]
    public string carFuelType;
    
    [JsonProperty("Fuel_consumption")]
    public string carConsumption;

    [JsonProperty("Fuel_Capacity")]
    public string carFuelCapacity;
}
