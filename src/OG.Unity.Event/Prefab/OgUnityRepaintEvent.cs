using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
namespace OG.Unity.Event.Prefab;
public class OgUnityRepaintEvent(IOgGraphicsTool graphicsTool) : OgUnityUnconsumeEvent, IOgRepaintEvent
{
    public          IOgGraphicsTool GraphicsTool                   { get; set; } = graphicsTool;
}