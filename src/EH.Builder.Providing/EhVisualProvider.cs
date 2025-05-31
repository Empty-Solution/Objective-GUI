using EH.Builder.Providing.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhVisualProvider(Font font, Material material, Texture2D logoTexture) : IEhVisualProvider
{
    public Font      Font        { get; set; } = font;
    public Material  Material    { get; set; } = material;
    public Texture2D LogoTexture { get; }      = logoTexture;
}