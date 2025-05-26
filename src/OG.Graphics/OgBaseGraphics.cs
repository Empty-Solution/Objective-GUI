using OG.Graphics.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Graphics;
public abstract class OgBaseGraphics<TContext> : IOgGraphics<TContext> where TContext : class, IOgGraphicsContext
{
    public bool CanHandle(IOgGraphicsContext value) => value is TContext;
    public void ProcessContext(IOgGraphicsContext ctx) => ProcessContext((ctx as TContext)!);
    public abstract void ProcessContext(TContext context);
}