using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgMinSizeOption(float minWidth, float minHeight) : IOgTransformerOption
{
    public float MinWidth                        { get; set; } = minWidth;
    public float MinHeight                       { get; set; } = minHeight;
    public bool  CanHandle(IOgTransformer value) => value is OgMinSizeTransformer;
}