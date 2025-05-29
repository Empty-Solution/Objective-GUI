using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhWindowConfig
{
    public DkProperty<Color> BackgroundColorProperty   { get; }      = new(new Color32(20, 20, 20, 255));
    public DkProperty<Color> LogoColor                 { get; }      = new(Color.white);
    public float             Width                     { get; set; } = 680;
    public float             Height                    { get; set; } = 600;
    public float             WindowBorderRadius        { get; set; } = 15;
    public float             TabButtonsContainerOffset { get; set; } = 15;
    public float             ToolbarContainerHeight    { get; set; } = 75;
    public float             ToolbarContainerOffset    { get; set; } = 7;
    public float             LogoSize    { get; set; } = 40;
}