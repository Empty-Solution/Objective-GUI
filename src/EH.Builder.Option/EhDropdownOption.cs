using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhDropdownOption
{
    public EhDropdownOption()
    {
        BackgroundColorProperty      = new(new Color32(30, 30, 30, 255));
        BackgroundHoverColorProperty = new(new Color32(50, 50, 50, 255));
        TextColorProperty            = new(new Color32(255, 255, 255, 255));
        SelectedTextColorProperty    = new(new Color32(255, 255, 255, 255));
        BorderColorProperty          = new(new Color32(255, 255, 255, 255));
    }
    public DkProperty<Color> BackgroundColorProperty      { get; }
    public DkProperty<Color> BackgroundHoverColorProperty { get; }
    public DkProperty<Color> TextColorProperty            { get; }
    public DkProperty<Color> SelectedTextColorProperty    { get; }
    public DkProperty<Color> BorderColorProperty          { get; }
    public float             Width                        { get; set; } = 200;
    public float             Height                       { get; set; } = 30;
    public float             DropdownItemHeight           { get; set; } = 25;
    public float             DropdownWidth                { get; set; } = 25;
    public float             DropdownItemPadding          { get; set; } = 25;
    public float             BorderWidth                  { get; set; } = 1;
    public float             BorderRadius                 { get; set; } = 5;
    public int               TextFontSize                 { get; set; } = 14;
    public TextAnchor        TextAlignment                { get; set; } = TextAnchor.MiddleLeft;
}