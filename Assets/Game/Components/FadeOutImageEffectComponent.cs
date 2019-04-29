using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOutImageEffectComponent : EZS.Component
{
    [HideInInspector]
    public float Time;

    public bool Active;
    public AnimationCurve OpacityCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.3f, 1));
}
