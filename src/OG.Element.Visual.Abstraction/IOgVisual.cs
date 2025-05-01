using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction.Handlers;

namespace OG.Element.Visual.Abstraction;

public interface IOgVisual : IOgElement, IOgRepaintEventHandler
{
    IDkGetProvider<int>? ZOrder { get; }
}