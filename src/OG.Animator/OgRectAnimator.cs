using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Animator;
public class OgRectAnimator(IDkGetProvider<float>? speedProvider = null) : OgAnimator<Rect>(new(), speedProvider)
{
    protected override Rect GetValue(Rect targetValue, float time) => new(targetValue.position * time, targetValue.size * time);
}