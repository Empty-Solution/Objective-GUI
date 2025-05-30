using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhInteractableElementConfig
{
    public float             Height                          { get; set; } = 28;
    public float             Width                           { get; set; } = 250;
    public float             HorizontalPadding               { get; set; } = 10;
    public float             VerticalPadding                 { get; set; } = 10;
    public float             ModalItemHeight                 { get; set; } = 28;
    public float             ModalWidth                      { get; set; } = 120;
    public float             BindModalItemHeight             { get; set; } = 28;
    public float             BindModalWidth                  { get; set; } = 220;
    public int               ModalButtonTextFontSize         { get; set; } = 14;
    public float             ModalBackgroundBorder           { get; set; } = 5;
    public float             ModalButtonBorder               { get; set; } = 5;
    public TextAnchor        ModalButtonTextAlignment        { get; set; } = TextAnchor.MiddleCenter;
    public int               BindModalTextFontSize           { get; set; } = 13;
    public TextAnchor        BindModalTextAlignment          { get; set; } = TextAnchor.MiddleCenter;
    public DkProperty<Color> ModalBackgroundColor            { get; }      = new(new Color32(20, 20, 20, 255));
    public DkProperty<Color> ModalButtonBackgroundColor      { get; }      = new(new Color32(30, 30, 30, 255));
    public DkProperty<Color> ModalButtonBackgroundHoverColor { get; }      = new(new Color32(40, 40, 40, 255));
    public DkProperty<Color> ModalButtonTextColor            { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> BindModalTextColor              { get; }      = new(new Color32(255, 255, 255, 255));
    public int               BindTextFontSize                { get; set; } = 13;
    public TextAnchor        BindTextAlignment               { get; set; } = TextAnchor.MiddleCenter;
    public DkProperty<Color> BindTextColor                   { get; }      = new(new Color32(255, 255, 255, 255));
    public float             BindWidth                       { get; set; } = 45;
    public float             BindHeight                      { get; set; } = 26;
    public float             BindBorder                      { get; set; } = 10;
    public DkProperty<Color> BindBackgroundColor             { get; }      = new(new Color32(30, 30, 30, 255));
    public DkProperty<Color> BindBackgroundHoverColor        { get; }      = new(new Color32(40, 40, 40, 255));
    
}