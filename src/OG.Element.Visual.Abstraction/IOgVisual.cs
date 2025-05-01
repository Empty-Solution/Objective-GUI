using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;

namespace OG.Element.Visual.Abstraction;

public interface IOgVisual : IOgElement
{
    IDkGetProvider<int>? ZOrder { get; }
}