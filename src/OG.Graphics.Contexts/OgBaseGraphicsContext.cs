using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics.Contexts;
public class OgBaseGraphicsContext : IOgGraphicsContext
{
    public Rect ClipRect   { get; set; }
    public Rect RenderRect { get; set; }
    public int  ZOrder     { get; set; }
}