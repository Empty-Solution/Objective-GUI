using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
namespace OG.Element.Visual.Abstraction;
public interface IOgVisual<TEvent, TReturn> : IOgElement, IOgRepaintEventHandler<TEvent, TReturn> where TEvent : IOgRepaintEvent
{
    IDkGetProvider<int>? ZOrder { get; }
}