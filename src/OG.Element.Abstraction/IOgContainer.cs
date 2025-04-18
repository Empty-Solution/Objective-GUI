using System.Collections.Generic;

namespace OG.Element.Abstraction;

public interface IOgContainer<TElement> : IOgContainer where TElement : IOgElement
{
    new IReadOnlyList<TElement> Children { get; }

    void AddChild(TElement child);

    void RemoveChild(TElement child);
}

public interface IOgContainer : IOgElement
{
    IEnumerable<IOgElement> Children { get; }

    bool ContainsChild(string name);

    void AddChild(IOgElement child);

    void RemoveChild(IOgElement child);
}