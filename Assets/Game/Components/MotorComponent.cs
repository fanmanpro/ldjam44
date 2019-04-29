using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MotorComponent : EZS.Component
{
    public float Torque;
    public float MaxRPM;
}
