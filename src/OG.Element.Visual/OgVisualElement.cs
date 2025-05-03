using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.Visual;
public abstract class OgVisualElement<TEvent, TReturn> : OgElement, IOgVisual<TEvent, TReturn> where TEvent : class, IOgRepaintEvent
{
    protected OgVisualElement(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgRepaintEventHandler(this));
    public          IDkGetProvider<int>? ZOrder { get; set; }
    public abstract TReturn              HandleRepaint(TEvent reason);
    public class OgRepaintEventHandler(IOgVisual<TEvent, TReturn> owner) : OgEventHandlerBase<TEvent>
    {
        public override bool Handle(TEvent reason)
        {
            owner.HandleRepaint(reason);
            return true;
        }
    }
}