using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterComponent : EZS.Component
{
    public Vector2 DesiredMovementVector;
    public float MovementStrength;
    public float MovementSpeed;
    public float JumpVelocity;
    public float JumpCooldown = 1;
}
