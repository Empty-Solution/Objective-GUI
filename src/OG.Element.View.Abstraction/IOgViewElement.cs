using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.View.Abstraction;
public interface IOgViewElement<out TViewValue> : IOgElement
{
    IDkGetProvider<TViewValue> View { get; }
}