using DK.Property.Generic;
using EH.Builder.Config.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhInteractableElementConfig : IEhElementConfig
{
    public float             HorizontalPadding               { get; set; } = 10;
    public float             VerticalPadding                 { get; set; } = 5;
    public float             ModalItemHeight                 { get; set; } = 22;
    public float             ModalWidth                      { get; set; } = 100;
    public float             ModalBackgroundBorder           { get; set; } = 5;
    public float             ModalButtonBorder               { get; set; } = 5;
    public int               ModalButtonTextFontSize         { get; set; } = 13;
    public TextAnchor        ModalButtonTextAlignment        { get; set; } = TextAnchor.MiddleCenter;
    public float             BindModalWidth                  { get; set; } = 220;
    public int               BindModalTextFontSize           { get; set; } = 12;
    public TextAnchor        BindModalTextAlignment          { get; set; } = TextAnchor.MiddleLeft;
    public float             BindWidth                       { get; set; } = 55;
    public float             BindHeight                      { get; set; } = 26;
    public float             BindBorder                      { get; set; } = 5;
    public int               BindTextFontSize                { get; set; } = 12;
    public TextAnchor        BindTextAlignment               { get; set; } = TextAnchor.MiddleCenter;
    public DkProperty<Color> ModalBackgroundColor            { get; }      = new(new Color32(20, 20, 20, 255));
    public DkProperty<Color> ModalButtonBackgroundColor      { get; }      = new(new Color32(30, 30, 30, 255));
    public DkProperty<Color> ModalButtonBackgroundHoverColor { get; }      = new(new Color32(40, 40, 40, 255));
    public DkProperty<Color> ModalButtonTextColor            { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> BindModalTextColor              { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> BindTextColor                   { get; }      = new(new Color32(255, 255, 255, 255));
    public DkProperty<Color> BindBackgroundColor             { get; }      = new(new Color32(25, 25, 25, 255));
    public DkProperty<Color> BindBackgroundHoverColor        { get; }      = new(new Color32(40, 40, 40, 255));
    public float             Height                          { get; set; } = 26;
    public float             Width                           { get; set; } = 250;
}