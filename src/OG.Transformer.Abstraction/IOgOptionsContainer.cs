namespace OG.Transformer.Abstraction;
public interface IOgOptionsContainer
{
    void SetOption<TValue>(string name, TValue value);
    bool RemoveOption(string name);
    bool TryGetValue<TValue>(string name, out TValue value);
}