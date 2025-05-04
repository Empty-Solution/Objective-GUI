using DK.Getting.Abstraction.Generic;
namespace OG.Element.Visual.Abstraction;
public interface IOgText : IOgVisual
{
    IDkGetProvider<string>? Text { get; set; }
}
