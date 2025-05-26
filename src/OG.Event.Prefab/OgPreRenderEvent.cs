using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
namespace OG.Event.Prefab;
public class OgPreRenderEvent(IEnumerable<IOgGraphics> graphics) : OgRenderEvent(graphics), IOgPreRenderEvent;