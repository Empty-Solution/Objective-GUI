using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using UnityEngine;

namespace OG.Common.Scoping;

public class OgClipTransformScope : OgGuiMatrixApplyTransformScope, IOgClipTransformScope
{
    public Vector2 ScrollPosition { get; set; }

    protected override void OnOpened(IOgTransform focus)
    {
        base.OnOpened(focus);
        GUI.BeginClip(focus.LocalRect, ScrollPosition, Vector2.zero, false);
    }

    protected override void OnClosed(IOgTransform focus)
    {
        GUI.EndClip();
        base.OnClosed(focus);
    }
}