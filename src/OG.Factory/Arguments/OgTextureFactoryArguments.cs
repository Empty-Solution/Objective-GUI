using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color, Material material) : OgVisualFactoryArguments(name, rectGetProvider, color)
{
    public Material Material => material;
}