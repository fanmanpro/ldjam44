using UnityEngine;
using EZS;

public class CharacterJumpSystem : EZS.System
{
    private CharacterComponent characterComponent;
    private JumpComponent jumpComponent;
    private Rigidbody2D rigidbodyComponent;

    private float jumpTimer;

    public enum CharacterJumpEvents
    {
        Jump
    }

    public override void InitSystem()
    {
        RegisterEventEnum<CharacterJumpEvents>();
        RegisterComponent<CharacterComponent>(a => a.PushComponent(ref characterComponent, ref jumpComponent, ref rigidbodyComponent), b => b.PopComponent(ref characterComponent, ref jumpComponent, ref rigidbodyComponent));
    }

    [EnumAction(typeof(CharacterJumpEvents))]
    public void InvokeCharacterJumpEvent(int e)
    {
        InvokeEventsOnComponents(AsInt(e), characterComponent);
    }

    protected override void UpdateSystem()
    {
        if(characterComponent.HasEvent(AsInt(CharacterJumpEvents.Jump)) && (jumpTimer + jumpComponent.JumpCooldown) <= Time.time && (jumpComponent.Collisions.Count > 0))
        {
            jumpTimer = Time.time;
            // apply the velocity
            rigidbodyComponent.velocity = new Vector2(rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.y + jumpComponent.JumpVelocity);
        }
    }
}