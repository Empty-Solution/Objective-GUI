namespace OG.Factory.Abstraction;

public interface IOgContentFactoryArguments<TContent> : IOgElementFactoryArguments
{
    TContent Content { get; }
}