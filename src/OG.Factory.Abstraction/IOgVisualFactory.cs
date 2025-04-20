using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgVisualFactory<TContent, TArguments, TScope> : IOgFactory<IOgVisualContent<TContent>, TArguments, TScope> where TScope : IOgTransformScope
    where TArguments : IOgContentFactoryArguments<TContent, TScope>;