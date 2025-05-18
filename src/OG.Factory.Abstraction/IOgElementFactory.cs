using DK.Factory.Abstraction;
using OG.Element.Abstraction;
namespace OG.Factory.Abstraction;
public interface IOgElementFactory<out TProduct, in TArguments> : IDkFactory<TProduct, TArguments>
    where TProduct : IOgElement where TArguments : IDkFactoryArguments;