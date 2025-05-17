using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using UnityEngine;
namespace OG.Animator;
public abstract class OgAnimator<TValue>(TValue initial, IDkGetProvider<float>? speedProvider = null) : IOgAnimator<TValue> where TValue : notnull
{
    protected float                  m_Time;
    public    IDkGetProvider<float>? Speed { get; }                = speedProvider;
    public    TValue                 Value { get; protected set; } = initial;
    public void Animate(float deltaTime, TValue targetValue)
    {
        m_Time += deltaTime * (Speed?.Get() ?? 1f);
        m_Time =  Mathf.Clamp01(m_Time);
        Value  =  GetValue(targetValue, m_Time);
    }
    protected abstract TValue GetValue(TValue targetValue, float time);
}