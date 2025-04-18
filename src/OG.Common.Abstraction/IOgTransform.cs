using UnityEngine;

namespace OG.Common.Abstraction;

public interface IOgTransform
{
    Rect LocalRect { get; set; }

    float Depth { get; }

    Vector3 Scale { get; set; }

    Quaternion Rotation { get; set; }
}