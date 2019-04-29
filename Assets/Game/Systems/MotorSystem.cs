using EZS;
using UnityEngine;

public class MotorSystem : EZS.System
{
    private MotorComponent[] motorComponents;
    private Rigidbody2D[] rigidbodyComponents;

    public override void InitSystem()
    {
        RegisterComponents<MotorComponent>(s => s.PushComponent(ref motorComponents, ref rigidbodyComponents), s => s.PopComponent(ref motorComponents, ref rigidbodyComponents));
    }

    protected override void UpdateSystem()
    {
        for(int m = 0; m < motorComponents.Length; m++)
        {
            MotorComponent motorComponent = motorComponents[m];
            Rigidbody2D rigidbodyComponent = rigidbodyComponents[m];

            if(rigidbodyComponent.velocity.magnitude < motorComponent.MaxRPM)
            {
				rigidbodyComponent.AddTorque(motorComponent.Torque);
            }
        }
    }
}
