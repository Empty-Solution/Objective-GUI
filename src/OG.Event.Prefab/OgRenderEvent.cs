using DK.Matching;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IEnumerable<IOgGraphics> graphics) : OgEvent, IOgRenderEvent
{
    private readonly DkTypeCacheMatcherProvider<IOgGraphicsContext, IOgGraphics> m_Provider = new(graphics);
    public           IEnumerable<IOgGraphics>                                    Graphics => graphics;
    public void Enter(Rect rect)
    {
        //GUI.BeginClip(rect);
    }
    public void Exit()
    {
        //GUI.EndClip();
    }
    public IOgGraphics GetGraphics(IOgGraphicsContext context) => m_Provider.TryGetMatcher(context, out IOgGraphics graphic) ? graphic : null!;
}