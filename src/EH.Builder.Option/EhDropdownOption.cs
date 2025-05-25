using DK.Property.Generic;
using UnityEngine;

namespace EH.Builder.Option;

public class EhDropdownOption
{
    private readonly Color m_BackgroundColor      = new Color32(30, 30, 30, 255);
    private readonly Color m_BackgroundHoverColor = new Color32(50, 50, 50, 255);
    private readonly Color m_TextColor            = new Color32(255, 255, 255, 255);
    private readonly Color m_SelectedTextColor    = new Color32(255, 255, 255, 255);
    private readonly Color m_BorderColor          = new Color32(255, 255, 255, 255);

    public EhDropdownOption()
    {
        BackgroundColorProperty      = new(m_BackgroundColor);
        BackgroundHoverColorProperty = new(m_BackgroundHoverColor);
        TextColorProperty            = new(m_TextColor);
        SelectedTextColorProperty    = new(m_SelectedTextColor);
        BorderColorProperty          = new(m_BorderColor);
    }

    public DkProperty<Color> BackgroundColorProperty      { get; }
    public DkProperty<Color> BackgroundHoverColorProperty { get; }
    public DkProperty<Color> TextColorProperty            { get; }
    public DkProperty<Color> SelectedTextColorProperty    { get; }
    public DkProperty<Color> BorderColorProperty          { get; }

    public float      Width               { get; set; } = 200;
    public float      Height              { get; set; } = 30;
    public float      DropdownItemHeight  { get; set; } = 25;
    public float      DropdownWidth       { get; set; } = 25;
    public float      DropdownItemPadding { get; set; } = 25;
    public float      BorderWidth         { get; set; } = 1;
    public float      BorderRadius        { get; set; } = 5;
    public int        TextFontSize        { get; set; } = 14;
    public TextAnchor TextAlignment       { get; set; } = TextAnchor.MiddleLeft;
}