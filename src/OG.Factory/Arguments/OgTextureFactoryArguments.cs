using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, Color color,
    Material material, Vector4 borders) : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, color)
{
    public Material Material => material;
    public Vector4  Borders  => borders;
}