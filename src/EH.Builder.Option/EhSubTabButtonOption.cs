using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhSubTabButtonOption
{
    public float             Height                  { get; set; } = 30;
    public float             Width                   { get; set; } = 50;
    public float             Padding                 { get; set; } = 50;
    public int               FontSize                { get; set; } = 20;
    public TextAnchor        Alignment               { get; set; } = TextAnchor.MiddleCenter;
    public DkProperty<Color> BackgroundColor         { get; }      = new(new Color32(50, 50, 50, 255));
    public DkProperty<Color> BackgroundHoverColor    { get; }      = new(new Color32(70, 70, 70, 255));
    public DkProperty<Color> BackgroundInteractColor { get; }      = new(new Color32(90, 90, 90, 255));
    public DkProperty<Color> TextColor               { get; }      = new(new Color32(200, 200, 200, 255));
    public DkProperty<Color> TextHoverColor          { get; }      = new(new Color32(255, 255, 255, 255));
    public float             Border                  { get; set; } = 90f;
}