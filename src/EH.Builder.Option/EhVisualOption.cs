using EH.Builder.Option.Abstraction;
using UnityEngine;
namespace EH.Builder.Option;
public class EhVisualOption(Font font) : IEhVisualOption
{
    public Font Font { get; set; } = font;
}