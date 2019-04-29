using UnityEngine;
using EZS;
using System.Linq;

public class CharacterSystem : EZS.System
{
    private CharacterComponent characterComponent;
    private Rigidbody2D rigidbodyComponent;
    private Animator animatorComponent;

    private const string animatorParamaterFacingLeft = "FacingLeft";

    private const float baseMovementSpeedModifier = 1.0f;

    private Vector2 forceVector;

    private float jumpTimer;

    public enum CharacterEvents
    {
        MoveLeft,
        MoveRight,
        StopMoveLeft,
        StopMoveRight,
        Jump
    }

    public override void InitSystem()
    {
        RegisterEventEnum<CharacterEvents>();
        RegisterComponent<CharacterComponent>(a => a.PushComponent(ref characterComponent, ref rigidbodyComponent, ref animatorComponent), b => b.PopComponent(ref characterComponent, ref rigidbodyComponent, ref animatorComponent));
    }

    [EnumAction(typeof(CharacterEvents))]
    public void InvokeCharacterEvent(int e)
    {
        InvokeEventsOnComponents(AsInt(e), characterComponent);
    }

    protected override void UpdateSystem()
    {
        if(characterComponent.HasEvent(AsInt(CharacterEvents.MoveLeft)))
        {
            characterComponent.DesiredMovementVector -= new Vector2(1, 0);
            animatorComponent.SetBool(animatorParamaterFacingLeft, true);
        } else if(characterComponent.HasEvent(AsInt(CharacterEvents.MoveRight)))
        {
            characterComponent.DesiredMovementVector += new Vector2(1, 0);
            animatorComponent.SetBool(animatorParamaterFacingLeft, false);
        }

        // prevents the key release bug before the scene has loaded
        if(characterComponent.DesiredMovementVector.x != 0)
        {
            if(characterComponent.HasEvent(AsInt(CharacterEvents.StopMoveLeft)))
            {
                characterComponent.DesiredMovementVector += new Vector2(1, 0);
            } else if(characterComponent.HasEvent(AsInt(CharacterEvents.StopMoveRight)))
            {
                characterComponent.DesiredMovementVector -= new Vector2(1, 0);
            }
        }

        if(characterComponent.HasEvent(AsInt(CharacterEvents.Jump)) && (jumpTimer + characterComponent.JumpCooldown) <= Time.time)
        {
            jumpTimer = Time.time;
            rigidbodyComponent.velocity = new Vector2(rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.y + characterComponent.JumpVelocity);
        }

        // apply the velocities
        rigidbodyComponent.velocity = new Vector2(characterComponent.DesiredMovementVector.x * characterComponent.MovementSpeed * baseMovementSpeedModifier, rigidbodyComponent.velocity.y + characterComponent.DesiredMovementVector.y);
    }
}