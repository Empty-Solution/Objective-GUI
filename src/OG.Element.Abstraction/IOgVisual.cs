using OG.Style.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgVisual<TStyle> : IOgVisual where TStyle : IOgStyle
{
    TStyle Style { get; }
}

public interface IOgVisual : IOgElement;