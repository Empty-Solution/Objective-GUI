using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public abstract class OgBaseGraphics<TContext> : IOgGraphics<TContext> where TContext : class, IOgGraphicsContext
{
    public Vector2 Global { get; set; } // временный костыль
    public abstract void Render(TContext ctx);
    public bool CanHandle(IOgGraphicsContext value) => value is TContext;
    public void Render(IOgGraphicsContext ctx) => Render((ctx as TContext)!);
}