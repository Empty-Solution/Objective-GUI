using OG.Transformer.Abstraction;
namespace OG.Transformer;
public class OgRelativeTransformerOption : IOgTransformerOption
{
    public float RelativeSize { get; set; }
    public int   Order        => 0;
    public bool CanHandle(IOgTransformer value) => value is OgRelativeTransformer;
}