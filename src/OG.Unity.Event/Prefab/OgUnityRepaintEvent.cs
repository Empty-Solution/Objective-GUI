#region

using OG.Event.Abstraction;
using OG.Graphics.Abstraction;

#endregion

namespace OG.Unity.Event.Prefab;

public class OgUnityRepaintEvent(IOgGraphicsTool graphicsTool) : OgUnityUnconsumeEvent, IOgRepaintEvent
{
    public IOgGraphicsTool GraphicsTool => graphicsTool;
}