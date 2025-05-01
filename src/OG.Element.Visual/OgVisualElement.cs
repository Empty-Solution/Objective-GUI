using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Abstraction;

namespace OG.Element.Visual;

public abstract class OgVisualElement : OgElement, IOgVisual
{
    protected OgVisualElement(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgRepaintEventHandler(this));

    public IDkGetProvider<int>? ZOrder { get; set; }

    public abstract bool HandleRepaint(IOgRepaintEvent reason);

    public class OgRepaintEventHandler(IOgVisual owner) : OgEventHandlerBase<IOgRepaintEvent>
    {
        public override bool Handle(IOgRepaintEvent reason) => owner.HandleRepaint(reason);
    }
}