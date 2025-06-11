using System.Collections.Generic;
using UnityEngine;
namespace OG.Graphics.Abstraction;
public struct OgClipContext(Rect clipRect, Vector2 global, Vector2 scrollOffset)
{
    public Rect    OriginalClipRect => clipRect;
    public Vector2 Global           { get; set; } = global;
    public Vector2 ScrollOffset     => scrollOffset;
    public Rect GetRectToClip() => new(clipRect.position + Global, clipRect.size);
    public List<IOgGraphicsContext> Contexts     { get; }      = [];
}