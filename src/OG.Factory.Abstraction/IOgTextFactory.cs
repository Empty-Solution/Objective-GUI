namespace OG.Factory.Abstraction;

public interface IOgTextFactory<TArguments> : IOgVisualFactory<string, TArguments> where TArguments : IOgTextFactoryArguments;