using EH.Builder.Options.Abstraction;
using UnityEngine;
namespace EH.Builder.Options;
public class EhVisualProvider(Font font, Material material) : IEhVisualProvider
{
    public Font     Font     { get; set; } = font;
    public Material Material { get; set; } = material;
}