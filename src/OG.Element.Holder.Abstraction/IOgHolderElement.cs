using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Holder.Abstraction;
public interface IOgHolderElement<THolder> : IOgElement
{
    IDkObservableProperty<THolder> Holder { get; }
}