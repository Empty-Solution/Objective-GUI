namespace OG.Render.Abstraction;

public interface IOgGraphics<TContext>
{
    void Draw(TContext context);
}
