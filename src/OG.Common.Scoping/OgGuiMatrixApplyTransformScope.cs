using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Common.Scoping;

public class OgGuiMatrixApplyTransformScope : OgGuiMatrixTransformScope
{
    protected override Matrix4x4 GetMatrix(Matrix4x4 original, IOgTransform focus) =>
        original * Matrix4x4.TRS(new(0.0f, 0.0f, -Mathf.Abs(focus.Depth)), focus.Rotation, focus.Scale);
}