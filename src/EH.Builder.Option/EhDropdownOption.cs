using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhDropdownOption
{
    public EhDropdownOption()
    {
        BackgroundColor          = new(new Color32(35, 35, 35, 255));
        TextColor                = new(new Color32(255, 255, 255, 255));
        NameTextColor            = new(new Color32(255, 255, 255, 255));
        ItemTextColor            = new(new Color32(200, 200, 200, 255));
        SelectedItemTextColor    = new(new Color32(255, 255, 255, 255));
        ModalBackgroundColor     = new(new Color32(25, 25, 25, 255));
        ItemBackgroundColor      = new(new Color32(25, 25, 25, 255));
        ItemBackgroundHoverColor = new(new Color32(50, 50, 50, 255));
    }
    public DkProperty<Color> ModalBackgroundColor    { get; }
    public DkProperty<Color> BackgroundColor         { get; }
    public DkProperty<Color> ItemBackgroundColor      { get; }
    public DkProperty<Color> ItemBackgroundHoverColor { get; }
    public DkProperty<Color> ItemTextColor            { get; }
    public DkProperty<Color> TextColor                { get; }
    public DkProperty<Color> NameTextColor            { get; }
    public DkProperty<Color> SelectedItemTextColor    { get; }
    public float             Width                    { get; set; } = 100;
    public float             Height                   { get; set; } = 30;
    public float             ModalItemHeight          { get; set; } = 25;
    public float             ModalItemPadding         { get; set; } = 5;
    public float             ModalBorder              { get; set; } = 5;
    public float             BorderWidth              { get; set; } = 1;
    public float             Border                   { get; set; } = 4;
    public int               TextNameFontSize         { get; set; } = 14;
    public TextAnchor        TextNameAlignment        { get; set; } = TextAnchor.MiddleLeft;
    public int               TextFontSize             { get; set; } = 14;
    public TextAnchor        TextAlignment            { get; set; } = TextAnchor.MiddleCenter;
    public int               ItemTextFontSize         { get; set; } = 14;
    public TextAnchor        ItemTextAlignment        { get; set; } = TextAnchor.MiddleCenter;
}