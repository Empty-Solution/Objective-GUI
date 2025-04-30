#region

using OG.Graphics.Abstraction;

#endregion

namespace OG.Event.Abstraction;

public interface IOgRepaintEvent : IOgEvent
{
    IOgGraphicsTool GraphicsTool { get; }
}