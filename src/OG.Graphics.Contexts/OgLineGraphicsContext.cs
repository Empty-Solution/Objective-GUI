using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics.Contexts;
public class OgLineGraphicsContext : OgBaseGraphicsContext, IOgLineGraphicsContext
{
    public float   LineWidth     { get; set; }
    public Vector2 StartPosition { get; set; }
    public Vector2 EndPosition   { get; set; }
    public Color   Color         { get; set; }
}