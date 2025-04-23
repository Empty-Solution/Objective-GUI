using OG.Animation.Abstraction;

namespace OG.Animation;

public readonly struct OgCurveVertex(float time, float value) : IOgCurveVertex
{
    public float Time { get; } = time;
    public float Value { get; } = value;
}