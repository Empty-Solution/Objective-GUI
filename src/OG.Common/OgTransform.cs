using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Common;

public class OgTransform : IOgTransform
{
    public Rect LocalRect { get; set; }

    public float Depth { get; set; } = 1;

    public Vector3 Scale { get; set; } = Vector3.one;

    public Quaternion Rotation { get; set; } = Quaternion.identity;
}