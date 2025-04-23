using DK.Common.DataTypes.Abstraction;

namespace OG.Animation.Abstraction;

public interface IOgCurve : IDkRange<float>
{
    int Count { get; }
    IOgCurveVertex this[int index] { get; }
    IOgCurveVertex GetNearestVertex(float time);
    IOgCurveVertex AddVertex(float time, float value);
    void RemoveVertex(int index);
    bool RemoveVertex(float time);
}