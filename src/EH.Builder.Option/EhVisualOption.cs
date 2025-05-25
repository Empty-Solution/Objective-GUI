using EH.Builder.Option.Abstraction;
using UnityEngine;
namespace EH.Builder.Option;
public class EhVisualOption(Font font, Material material) : IEhVisualOption
{
    public Font     Font     { get; set; } = font;
    public Material Material { get; set; } = material;
}