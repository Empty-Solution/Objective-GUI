using OG.Animation.Abstraction;
using UnityEngine;

namespace OG.Animation;

public abstract class OgCurve(float min, float max) : IOgCurve
{
    private List<IOgCurveVertex> Vertices { get; } = [];
    public float Min { get; } = min;
    public float Max { get; } = max;
    public int Count => Vertices.Count;

    public IOgCurveVertex this[int index] =>
        Vertices.ElementAt(index);

    public abstract IOgCurveVertex GetNearestVertex(float time);

    public IOgCurveVertex AddVertex(float time, float value)
    {
        time = Mathf.Clamp01(time);
        value = Mathf.Clamp(value, Min, Max);
        OgCurveVertex vertex = new(time, value);
        Vertices.Add(vertex);
        return vertex;
    }

    public void RemoveVertex(int index) =>
        Vertices.RemoveAt(index);

    public bool RemoveVertex(float time) =>
        Vertices.RemoveAll(x => Mathf.Approximately(x.Time, time)) != 0;
}