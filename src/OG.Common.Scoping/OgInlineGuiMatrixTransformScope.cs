using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Common.Scoping;

public class OgInlineGuiMatrixTransformScope : OgGuiMatrixApplyTransformScope
{
    protected override Matrix4x4 GetMatrix(Matrix4x4 original, IOgTransform focus) =>
        base.GetMatrix(original, focus) * Matrix4x4.Translate(focus.LocalRect.position);
}