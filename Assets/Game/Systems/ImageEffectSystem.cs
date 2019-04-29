using EZS;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageEffectSystem : EZS.System
{
    private FadeOutImageEffectComponent fadeOutImageEffectComponent;
    private FadeInImageEffectComponent fadeInImageEffectComponent;

    private Image fadeInImageComponent;
    private Image fadeOutImageComponent;

    private float fadeInDuration, fadeOutDuration;

    public enum ImageEffectEvents
    {
        FadeIn,
        FadeOut
    }

    public override void InitSystem()
    {
        RegisterComponent<FadeInImageEffectComponent>(s => s.PushComponent(ref fadeInImageEffectComponent, ref fadeInImageComponent), s => s.PopComponent(ref fadeInImageEffectComponent, ref fadeInImageComponent));
        RegisterComponent<FadeOutImageEffectComponent>(s => s.PushComponent(ref fadeOutImageEffectComponent, ref fadeOutImageComponent), s => s.PopComponent(ref fadeOutImageEffectComponent, ref fadeOutImageComponent));
    }

    public override void Ready()
    {
        fadeInDuration = fadeInImageEffectComponent.OpacityCurve.keys.Last().time;
        fadeOutDuration = fadeOutImageEffectComponent.OpacityCurve.keys.Last().time;
    }

    [EnumAction(typeof(ImageEffectEvents))]
    public void InvokeImageEffectEvent(int e)
    {
        InvokeEventsOnComponents(AsInt(e), fadeInImageEffectComponent);
        InvokeEventsOnComponents(AsInt(e), fadeOutImageEffectComponent);
    }

    protected override void UpdateSystem()
    {
        if(fadeInImageEffectComponent.Active)
        {
            fadeInImageEffectComponent.Time += Time.deltaTime;
            if(fadeInDuration > fadeInImageEffectComponent.Time)
            {
                fadeInImageComponent.enabled = true;
                fadeInImageComponent.color = new Color(fadeInImageComponent.color.r, fadeInImageComponent.color.g, fadeInImageComponent.color.b, fadeInImageEffectComponent.OpacityCurve.Evaluate(fadeInImageEffectComponent.Time % fadeInDuration));
            } else
            {
                fadeInImageComponent.color = new Color(fadeInImageComponent.color.r, fadeInImageComponent.color.g, fadeInImageComponent.color.b, fadeInImageEffectComponent.OpacityCurve.Evaluate(fadeInDuration));
                fadeInImageEffectComponent.Active = false;
                fadeInImageEffectComponent.Time = 0;
                // to disable the raycast target
                fadeInImageComponent.enabled = false;
            }
        } else
        {
            // its ok if we only start the transition on the next frame
            if(fadeInImageEffectComponent.HasEvent(AsInt(ImageEffectEvents.FadeIn)))
            {
                fadeInImageEffectComponent.Active = true;
            }
        }
        if(fadeOutImageEffectComponent.Active)
        {
            fadeOutImageEffectComponent.Time += Time.deltaTime;
            if(fadeOutDuration > fadeOutImageEffectComponent.Time)
            {
                fadeOutImageComponent.enabled = true;
                fadeOutImageComponent.color = new Color(fadeOutImageComponent.color.r, fadeOutImageComponent.color.g, fadeOutImageComponent.color.b, fadeOutImageEffectComponent.OpacityCurve.Evaluate(fadeOutImageEffectComponent.Time % fadeOutDuration));
            } else
            {
                fadeOutImageComponent.color = new Color(fadeOutImageComponent.color.r, fadeOutImageComponent.color.g, fadeOutImageComponent.color.b, fadeOutImageEffectComponent.OpacityCurve.Evaluate(fadeOutDuration));
                fadeOutImageEffectComponent.Active = false;
                fadeOutImageEffectComponent.Time = 0;
            }
        } else
        {
            // its ok if we only start the transition on the next frame
            if(fadeOutImageEffectComponent.HasEvent(AsInt(ImageEffectEvents.FadeOut)))
            {
                fadeOutImageEffectComponent.Active = true;
            }
        }
    }
}
