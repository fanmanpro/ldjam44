using UnityEngine;
using UnityEngine.Events;

public class JumpComponent : ColliderComponent<SurfaceComponent>
{
    public float JumpVelocity;
    public float JumpCooldown = 1;
}
