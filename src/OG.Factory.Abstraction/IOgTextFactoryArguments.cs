using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgTextFactoryArguments<TScope> : IOgContentFactoryArguments<string, TScope> where TScope : IOgTransformScope;