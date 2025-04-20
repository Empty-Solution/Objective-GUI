using OG.Common;
using OG.Common.Abstraction;
using OG.Factory.Abstraction;
using UnityEngine;

namespace OG.Factory.General;

public class OgTransformFactory : IOgTransformFactory
{
    public IOgTransform Create(OgTransformFactoryArguments arguments)
    {
        arguments.Destructor(out Rect rect, out Quaternion rotation, out Vector3 scale, out float depth);
        return InternalCreate(rect, rotation, scale, depth);
    }

    private IOgTransform InternalCreate(Rect rect, Quaternion rotation, Vector3 scale, float depth) => new OgTransform { LocalRect = rect, Rotation = rotation, Scale = scale, Depth = depth };
}