using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhWindowOption
{
    private readonly Color m_BackgroundColor = new Color32(20, 20, 20, 255);
    public EhWindowOption() => BackgroundColorProperty = new(m_BackgroundColor);
    public DkProperty<Color> BackgroundColorProperty   { get; }
    public float             Width               { get; set; } = 668;
    public float             Height              { get; set; } = 600;
    public float             WindowBorderRadius        { get; set; } = 15;
    public float             TabButtonsContainerOffset { get; set; } = 15;
    public float             ToolbarContainerHeight    { get; set; } = 75;
    public float             ToolbarContainerOffset    { get; set; } = 7;
}