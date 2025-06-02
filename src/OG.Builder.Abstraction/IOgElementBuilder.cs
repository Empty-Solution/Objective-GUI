using OG.Element.Abstraction;
namespace OG.Builder.Abstraction;
public interface IOgElementBuilder<in TArgs, out TOutput> where TArgs : IOgBuildArguments where TOutput : IOgElement
{
    TOutput Build(TArgs args);
}