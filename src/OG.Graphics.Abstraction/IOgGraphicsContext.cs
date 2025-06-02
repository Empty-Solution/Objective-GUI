using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsContext
{
    Rect ClipRect   { get; set; }
    Rect RenderRect { get; set; }
    int  ZOrder     { get; set; }
}