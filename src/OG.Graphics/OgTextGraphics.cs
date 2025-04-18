using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Graphics;

public class OgTextGraphics<TStyle> : IOgGraphics<IOgTextGraphicsContext<TStyle>> where TStyle : IOgTextStyle
{
    private const int CONTROLL_ID = -1;
    private static readonly GUIStyle tempStyle = new();
    private static readonly GUIContent tempContent = new();

    public void Draw(IOgTextGraphicsContext<TStyle> context)
    {
        context.Style.FillUnityStyle(tempStyle);
        tempContent.text = context.Content;
        tempStyle.Draw(context.Rect, tempContent, CONTROLL_ID);
    }
}