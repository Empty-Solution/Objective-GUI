using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color, Material material, Vector4 borders)
    : OgVisualFactoryArguments(name, rectGetProvider, color)
{
    public Material Material => material;
    public Vector4  Borders  => borders;
}