using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Animator;
public class OgColorAnimator(IDkGetProvider<float>? speedProvider = null) : OgAnimator<Color>(new(), speedProvider)
{
    protected override Color GetValue(Color targetValue, float time) =>
        new(targetValue.r * time, targetValue.g * time, targetValue.b * time, targetValue.a * time);
}