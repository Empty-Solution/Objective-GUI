using DK.Common.Factory.Abstraction;
using UnityEngine;

namespace OG.Factory.Abstraction;

public readonly struct OgTransformFactoryArguments(Rect rect, Quaternion rotation, Vector3 scale, float depth) : IDkFactoryArguments
{
    public Rect Rect => rect;

    public Quaternion Rotation => rotation;

    public Vector3 Scale => scale;

    public float Depth => depth;

    public void Destructor(out Rect rect, out Quaternion rotation, out Vector3 scale, out float depth)
    {
        rect = Rect;
        rotation = Rotation;
        scale = Scale;
        depth = Depth;
    }
}