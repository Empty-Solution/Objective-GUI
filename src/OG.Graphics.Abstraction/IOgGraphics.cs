using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgGraphics
{
    Vector2 Global { get; set; }
    void Render(IOgGraphicsContext ctx);
}