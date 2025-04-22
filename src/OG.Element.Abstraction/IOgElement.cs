using OG.Common.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgElement
{
    string Name { get; }
    
    bool Active { get; set; }

    IOgTransform Transform { get; }

    void OnGUI(OgEvent reason);
}