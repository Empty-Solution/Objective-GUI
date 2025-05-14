using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement(string name, IOgEventHandlerProvider provider, IOgOptionsContainer options) : IOgElement, IOgEventCallback<IOgLayoutEvent>
{
    public IOgOptionsContainer Options     => options;
    public string              Name        => name;
    public bool                IsActive    { get; set; } = true;
    public Rect                ElementRect { get; protected set; }
    public bool ProcessEvent(IOgEvent reason) => IsActive && provider.Handle(reason);
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        ElementRect = reason.Layout.ProcessLayout(options);
        return false;
    }
}