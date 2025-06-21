using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgQuadFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> topLeft, IDkGetProvider<Color> topRight, IDkGetProvider<Color> bottomLeft, IDkGetProvider<Color> bottomRight,
    IDkGetProvider<Material?> material) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkGetProvider<Color> TopLeft { get; } = topLeft;
    public IDkGetProvider<Color> TopRight { get; } = topRight;
    public IDkGetProvider<Color> BottomLeft { get; } = bottomLeft;
    public IDkGetProvider<Color> BottomRight { get; } = bottomRight;
    public IDkGetProvider<Material?> Material { get; } = material;
}