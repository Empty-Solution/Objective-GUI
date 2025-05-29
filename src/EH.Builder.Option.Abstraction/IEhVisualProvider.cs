using UnityEngine;
namespace EH.Builder.Options.Abstraction;
public interface IEhVisualProvider
{
    Font      Font        { get; }
    Material  Material    { get; }
    Texture2D LogoTexture { get; }
}