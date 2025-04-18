using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Common.Scoping;

public abstract class OgGuiMatrixTransformScope : OgTransformScope
{
    protected override void OnClosed(IOgTransform focus) =>
        GL.PopMatrix();

    protected override void OnOpened(IOgTransform focus)
    {
        GL.PushMatrix();
        GL.MultMatrix(GetMatrix(GL.modelview, focus));
    }

    protected abstract Matrix4x4 GetMatrix(Matrix4x4 original, IOgTransform focus);
}