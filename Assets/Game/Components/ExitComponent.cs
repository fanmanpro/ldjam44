using UnityEngine;
using UnityEngine.Events;

public class ExitComponent : TriggerComponent<CharacterComponent>
{
    // used to keep track of how long the player is at the door
    [HideInInspector]
    public float Time;

    public bool Exiting;
    public float TimeToExit;
    public UnityEvent SuccessExit;
}
