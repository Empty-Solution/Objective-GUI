#region

using OG.Element.Abstraction;
using OG.Element.Hoverable.Abstraction;

#endregion

namespace OG.Element.Control.Abstraction;

public interface IOgControl<TElement> : IOgHoverable<TElement> where TElement : IOgElement
{
    bool IsControlling { get; }
}