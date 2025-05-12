using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgSizeOption(float width, float height) : IOgTransformerOption
{
    public float Width                           { get; set; } = width;
    public float Height                          { get; set; } = height;
    public bool  CanHandle(IOgTransformer value) => value is OgSizeTransformer;
}