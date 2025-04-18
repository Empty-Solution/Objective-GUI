using OG.Style.Abstraction;

namespace OG.Graphics.Abstraction;

public interface IOgStyledGraphicsContext<TContent, TStyle> : IOgContentGraphicsContext<TContent> where TStyle : IOgStyle
{
    TStyle Style { get; }
}