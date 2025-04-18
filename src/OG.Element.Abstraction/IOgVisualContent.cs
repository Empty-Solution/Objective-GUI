namespace OG.Element.Abstraction;

public interface IOgVisualContent<TContent> : IOgVisual
{
    TContent Content { get; set; }
}