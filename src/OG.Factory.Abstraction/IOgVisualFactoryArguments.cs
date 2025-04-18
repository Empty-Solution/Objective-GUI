namespace OG.Factory.Abstraction;

public interface IOgVisualFactoryArguments<TContent> : IOgFactoryArguments
{
    TContent Content { get; }
}
