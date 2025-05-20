using OG.DataTypes.Alignment;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgAlignmentTransformer : OgBaseTransformer<OgAlignmentTransformerOption>
{
    public override int Order { get; set; } = 1000;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgAlignmentTransformerOption option)
    {
        Vector2 position = rect.position;
    
        if (option.Alignment.HasFlag(EOgAlignment.CENTER))
            position.x = parentRect.x + (parentRect.width - rect.width) / 2;
        else if (option.Alignment.HasFlag(EOgAlignment.RIGHT))
            position.x = parentRect.x + parentRect.width - rect.width;
        else if (option.Alignment.HasFlag(EOgAlignment.LEFT))
            position.x = parentRect.x;
    
        if (option.Alignment.HasFlag(EOgAlignment.MIDDLE))
            position.y = parentRect.y + (parentRect.height - rect.height) / 2;
        else if (option.Alignment.HasFlag(EOgAlignment.BOTTOM))
            position.y = parentRect.y + parentRect.height - rect.height;
        else if (option.Alignment.HasFlag(EOgAlignment.TOP))
            position.y = parentRect.y;
    
        return new(position, rect.size);
    }
}