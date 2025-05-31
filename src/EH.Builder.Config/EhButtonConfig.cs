using DK.Property.Generic;
using EH.Builder.Config.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhButtonConfig : IEhElementConfig
{
    public float             BackgroundBorder        { get; set; } = 5f;
    public DkProperty<Color> BackgroundColor         { get; set; } = new(new(0.2f, 0.2f, 0.2f, 1f));
    public DkProperty<Color> BackgroundHoverColor    { get; set; } = new(new(0.3f, 0.3f, 0.3f, 1f));
    public DkProperty<Color> BackgroundInteractColor { get; set; } = new(new(0.15f, 0.15f, 0.15f, 1f));
    public DkProperty<Color> TextColor               { get; set; } = new(Color.white);
    public int               TextFontSize            { get; set; } = 14;
    public TextAnchor        TextAlignment           { get; set; } = TextAnchor.MiddleCenter;
    public float             Width                   { get; set; } = 60f;
    public float             Height                  { get; set; } = 28;
}