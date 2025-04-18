using OG.Common.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgElement
{
    string Name { get; }

    IOgTransform Transform { get; }

    void OnGUI(OgEvent reason);
}