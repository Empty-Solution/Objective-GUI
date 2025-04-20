using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgTextFactoryArguments<TScope> : IOgVisualFactoryArguments<string, TScope> where TScope : IOgTransformScope;