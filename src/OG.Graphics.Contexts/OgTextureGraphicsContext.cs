using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics.Contexts;
public class OgTextureGraphicsContext : OgBaseGraphicsContext, IOgTextureGraphicsContext
{
    public Color Color { get; set; }
    public ScaleMode ScaleMode { get; set; }
    public Texture2D? Texture { get; set; }
    public Vector4 BorderWidths { get; set; }
    public Vector4 BorderRadiuses { get; set; }
    public float ImageAspect { get; set; }
    public bool AlphaBlend { get; set; }
}