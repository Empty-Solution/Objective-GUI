using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgBlurTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color, Material material, Vector4 borders,
    float blurStrength) : OgTextureFactoryArguments(name, rectGetProvider, color, material, borders)
{
    public float BlurStrength => blurStrength;
}