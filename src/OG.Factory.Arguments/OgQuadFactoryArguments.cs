using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgQuadFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, Color topLeft,
    Color topRight, Color bottomLeft, Color bottomRight) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public Color TopLeft     { get; } = topLeft;
    public Color TopRight    { get; } = topRight;
    public Color BottomLeft  { get; } = bottomLeft;
    public Color BottomRight { get; } = bottomRight;
}