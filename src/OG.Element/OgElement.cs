using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement(string name, IOgEventHandlerProvider provider, IOgOptionsContainer options) : IOgElement, IOgEventCallback<IOgLayoutEvent>
{
    public         string              Name        => name;
    public         bool                IsActive    { get; set; } = true;
    public         Rect                ElementRect { get; protected set; }
    public         IOgOptionsContainer Options     => options;
    public virtual Rect                QueuedRect  { get; set; }
    public bool                ProcessEvent(IOgEvent reason) => IsActive && provider.Handle(reason);
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        Rect queuedRect  = QueuedRect;
        if(queuedRect != Rect.zero)
        {
            ElementRect = queuedRect;
            QueuedRect  = Rect.zero;
            return false;
        }
        Rect rect = new();
        foreach(IOgTransformer transformer in reason.Transformers)
        {
            if(!Options.TryGetOption(transformer, out IOgTransformerOption option)) continue;
            rect = transformer.Transform(rect, reason.ParentRect, reason.LastLayoutRect, reason.RemainingLayoutItems,
                                         option);
        }
        ElementRect           = rect;
        reason.LastLayoutRect = rect;
        return false;
    }
}