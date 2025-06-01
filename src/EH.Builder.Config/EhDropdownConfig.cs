using DK.Property.Generic;
using EH.Builder.Config.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhDropdownConfig : IEhElementConfig
{
    public DkProperty<Color> BackgroundColor          { get; }      = new(new Color32(35, 35, 35, 255));
    public DkProperty<Color> ItemBackgroundColor      { get; }      = new(new Color32(25, 25, 25, 255));
    public DkProperty<Color> ItemBackgroundHoverColor { get; }      = new(new Color32(50, 50, 50, 255));
    public DkProperty<Color> ItemTextColor            { get; }      = new(new Color32(200, 200, 200, 255));
    public DkProperty<Color> TextColor                { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> NameTextColor            { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> SelectedItemTextColor    { get; }      = new(new Color32(255, 255, 255, 255));
    public float             ModalItemHeight          { get; set; } = 25;
    public float             ModalItemPadding         { get; set; } = 5;
    public float             Border                   { get; set; } = 4;
    public int               NameTextFontSize         { get; set; } = 13;
    public TextAnchor        NameTextAlignment        { get; set; } = TextAnchor.MiddleLeft;
    public int               TextFontSize             { get; set; } = 13;
    public TextAnchor        TextAlignment            { get; set; } = TextAnchor.MiddleCenter;
    public int               ItemTextFontSize         { get; set; } = 13;
    public TextAnchor        ItemTextAlignment        { get; set; } = TextAnchor.MiddleCenter;
    public float             Width                    { get; set; } = 100;
    public float             Height                   { get; set; } = 24;
}