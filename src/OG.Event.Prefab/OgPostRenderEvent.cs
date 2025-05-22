using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
namespace OG.Event.Prefab;
public class OgPostRenderEvent(IEnumerable<IOgGraphics> graphics) : OgRenderEvent(graphics), IOgPostRenderEvent;