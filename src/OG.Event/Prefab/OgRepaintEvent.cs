using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
namespace OG.Event.Prefab;
public class OgRepaintEvent(IOgGraphicsTool graphicsTool) : OgUnconsumeEvent, IOgRepaintEvent
{
    public          IOgGraphicsTool GraphicsTool                   { get; set; } = graphicsTool;
}