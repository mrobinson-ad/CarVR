using UnityEngine;

public interface ICarInteraction 
{
    void ToggleDoor(DoorType doorType);
    void HonkHorn();
}

public enum DoorType
{
    FrontLeft,
    FrontRight,
    RearLeft,
    RearRight
}
