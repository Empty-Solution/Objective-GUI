namespace OG.Graphics.Abstraction;

public interface IOgContentGraphicsContext<TContent> : IOgGraphicsContext
{
    TContent Content { get; }
}