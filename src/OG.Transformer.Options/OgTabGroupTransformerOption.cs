using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgTabGroupTransformerOption(float xPadding, float yPadding, int rowSize = 2) : IOgTransformerOption
{
    public float XPadding => xPadding;
    public float YPadding => yPadding;
    public int RowSize => rowSize;
}