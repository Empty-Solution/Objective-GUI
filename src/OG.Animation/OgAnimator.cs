using OG.Animation.Abstraction;
using UnityEngine;

namespace OG.Animation;

public class OgAnimator(IOgCurve curve) : IOgAnimator
{
    private float m_Time;
    private float m_Value;
    public IOgCurve Curve { get; } = curve;
    public bool Reversed { get; set; }
    public bool Looped { get; set; }
    public float Scale { get; set; }
    public float Speed { get; set; }
    public float Value => m_Value * Scale;

    public void Animate(float deltaTime)
    {
        CapTime();
        m_Value = CalculateValue(ApplyModifications(deltaTime));
    }

    protected virtual void CapTime()
    {
        if(Looped && Reversed ? m_Time <= 0f : m_Time >= 1f)
            m_Time = Reversed ? 1f : 0f;
    }

    protected virtual float ApplyModifications(float deltaTime)
    {
        deltaTime *= Speed;
        if(Reversed)
            deltaTime = -deltaTime;
        return deltaTime;
    }

    protected virtual float CalculateValue(float deltaTime) =>
        Mathf.Lerp(m_Value, Curve.GetNearestVertex(m_Time += deltaTime).Value, deltaTime);
}