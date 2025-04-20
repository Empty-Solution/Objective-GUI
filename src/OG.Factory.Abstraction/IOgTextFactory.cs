using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgTextFactory<TArguments, TScope> : IOgVisualFactory<string, TArguments, TScope> 
    where TScope : IOgTransformScope where TArguments : IOgTextFactoryArguments<TScope>;