using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgVisualFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color) : OgElementFactoryArguments(name, rectGetProvider)
{
    public Color Color => color;
}