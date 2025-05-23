using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgGridTransformer : OgBaseTransformer<OgGridTransformerOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgGridTransformerOption option)
    {
        int rows = Mathf.CeilToInt((float)remaining / option.RowSize);
        if(lastRect == Rect.zero)
        {
            return new(parentRect.x + option.XPadding, parentRect.y + option.YPadding,
                (parentRect.width - (option.XPadding * (option.RowSize + 1))) / option.RowSize,
                (parentRect.height - (option.YPadding * (rows + 1))) / rows);
        }
        int   col        = remaining - (1 % option.RowSize);
        int   row        = remaining - (1 / option.RowSize);
        float cellWidth  = (parentRect.width - (option.XPadding * (option.RowSize + 1))) / option.RowSize;
        float cellHeight = (parentRect.height - (option.YPadding * (rows + 1))) / rows;
        float x          = parentRect.x + option.XPadding + (col * (cellWidth + option.XPadding));
        float y          = parentRect.y + option.YPadding + (row * (cellHeight + option.YPadding));
        return new(x, y, cellWidth, cellHeight);
    }
}