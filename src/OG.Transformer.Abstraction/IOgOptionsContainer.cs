namespace OG.Transformer.Abstraction;
public interface IOgOptionsContainer
{
    IOgOptionsContainer SetOption<TValue>(string name, TValue value);
    IOgOptionsContainer RemoveOption(string name);
    bool TryGetValue<TValue>(string name, out TValue value);
}