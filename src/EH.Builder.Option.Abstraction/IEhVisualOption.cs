using UnityEngine;
namespace EH.Builder.Option.Abstraction;
public interface IEhVisualOption
{
    Material Material { get; set; }
    Font     Font     { get; set; }
}