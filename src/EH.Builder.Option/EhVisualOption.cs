using EH.Builder.Option.Abstraction;
using UnityEngine;
namespace EH.Builder.Option;
public class EhVisualOption(Material material, Font font) : IEhVisualOption
{
    public Material Material { get; set; } = material;
    public Font     Font     { get; set; } = font;
}