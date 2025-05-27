using EH.Builder.Options.Abstraction;
using UnityEngine;
namespace EH.Builder.Options;
public class EhVisualOption(Font font, Material material) : IEhVisualOption
{
    public Font     Font     { get; set; } = font;
    public Material Material { get; set; } = material;
}