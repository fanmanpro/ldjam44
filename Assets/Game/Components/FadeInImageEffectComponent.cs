using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeInImageEffectComponent : EZS.Component
{
    [HideInInspector]
    public float Time;

    public bool Active;
    public AnimationCurve OpacityCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.3f, 0));
}
