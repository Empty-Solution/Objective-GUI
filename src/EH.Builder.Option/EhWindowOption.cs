using DK.Binding.Generic;
using DK.Property.Generic;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhWindowOption
{
    public readonly List<DkBinding<Color>> m_BackgroundColorBindings = [];
    private         Color                  m_BackgroundColor         = new Color32(20, 20, 20, 255);
    public EhWindowOption() =>
        BackgroundColorProperty = new(() => m_BackgroundColor, value =>
        {
            m_BackgroundColor = value;
            foreach(DkBinding<Color> binding in m_BackgroundColorBindings) binding.Sync();
        });
    public DkScriptableProperty<Color> BackgroundColorProperty   { get; }
    public float                       WindowWidth               { get; set; } = 668;
    public float                       WindowHeight              { get; set; } = 600;
    public float                       WindowBorderRadius        { get; set; } = 15;
    public float                       TabButtonsContainerOffset { get; set; } = 15;
    public float                       ToolbarContainerHeight    { get; set; } = 75;
    public float                       ToolbarContainerOffset    { get; set; } = 7;
}