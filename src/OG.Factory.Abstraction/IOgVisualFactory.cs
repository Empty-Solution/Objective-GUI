using OG.Element.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgVisualFactory<TContent, TArguments> : IOgFactory<IOgVisualContent<TContent>, TArguments> where TArguments : IOgVisualFactoryArguments<TContent>;