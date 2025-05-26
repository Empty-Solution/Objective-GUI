using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsContext
{
    Rect RenderRect { get; set; }
    int  ZOrder     { get; set; }
}