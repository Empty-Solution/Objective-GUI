namespace OG.Factory.Abstraction;

public interface IOgContentFactoryArguments<TContent> : IOgFactoryArguments
{
    TContent Content { get; }
}