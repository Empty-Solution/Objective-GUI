using OG.Element.Abstraction;
namespace OG.Builder.Abstraction;
public interface IOgElementBuilder<in TArgs> where TArgs : IOgBuildArguments
{
    IOgElement Build(TArgs args);
}