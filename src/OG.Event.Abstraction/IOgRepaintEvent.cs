using OG.Graphics.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgRepaintEvent : IOgEvent
{
    IOgGraphicsTool GraphicsTool { get; }
}