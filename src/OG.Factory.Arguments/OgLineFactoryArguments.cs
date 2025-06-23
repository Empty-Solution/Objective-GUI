using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgLineFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> colorGetter, IDkGetProvider<Vector2> startPosition, IDkGetProvider<Vector2> endPosition, IDkGetProvider<float> lineWidth)
    : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, colorGetter)
{
    public IDkGetProvider<Vector2> StartPosition { get; } = startPosition;
    public IDkGetProvider<Vector2> EndPosition { get; } = endPosition;
    public IDkGetProvider<float> LineWidth { get; } = lineWidth;
}