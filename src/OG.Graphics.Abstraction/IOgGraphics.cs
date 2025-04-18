namespace OG.Graphics.Abstraction;

public interface IOgGraphics<TContext> : IOgGraphics where TContext : IOgGraphicsContext
{
    void Draw(TContext context);
}

public interface IOgGraphics;