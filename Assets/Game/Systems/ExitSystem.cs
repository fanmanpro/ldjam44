using EZS;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSystem : EZS.System
{
    private ExitComponent exitComponent;
    private Animator animatorComponent;

    private bool playerEntered;

    private const string animatorParamaterExiting = "Exiting";

    public override void InitSystem()
    {
        RegisterComponent<ExitComponent>(s => s.PushComponent(ref exitComponent, ref animatorComponent), s => s.PopComponent(ref exitComponent, ref animatorComponent));
    }

    protected override void UpdateSystem()
    {
        if(exitComponent.Exiting)
        {
            return;
        }
        if(exitComponent.Triggers.Count > 0)
        {
            playerEntered = true;
            animatorComponent.SetBool(animatorParamaterExiting, true);
        }
        if(playerEntered)
        {
            if(exitComponent.Triggers.Count < 1)
            {
                exitComponent.Time = 0;
                animatorComponent.SetBool(animatorParamaterExiting, false);
                return;
            }
            exitComponent.Time += Time.deltaTime;
            if(exitComponent.Time >= exitComponent.TimeToExit)
            {
                exitComponent.SuccessExit.Invoke();
                exitComponent.Exiting = true;
                return;
            }
        }
    }
}
