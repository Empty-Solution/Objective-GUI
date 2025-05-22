using DK.Binding.Generic;
using DK.Property.Generic;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhSliderOption
{
    public readonly List<DkBinding<Color>> m_BackgroundColorBindings     = [];
    public readonly List<DkBinding<Color>> m_BackgroundFillColorBindings = [];
    public readonly List<DkBinding<Color>> m_TextColorBindings           = [];
    public readonly List<DkBinding<Color>> m_ThumbColorBindings          = [];
    public readonly List<DkBinding<Color>> m_ThumbOutlineColorBindings   = [];
    private         Color                  m_BackgroundColor             = Color.black;
    private         Color                  m_BackgroundFillColor         = Color.white;
    private         Color                  m_TextColor                   = Color.white;
    private         Color                  m_ThumbColor                  = Color.black;
    private         Color                  m_ThumbOutlineColor           = Color.white;
    public EhSliderOption()
    {
        BackgroundColorProperty = new(() => m_BackgroundColor, value =>
        {
            m_BackgroundColor = value;
            foreach(DkBinding<Color> binding in m_BackgroundColorBindings) binding.Sync();
        });
        BackgroundFillColorProperty = new(() => m_BackgroundFillColor, value =>
        {
            m_BackgroundFillColor = value;
            foreach(DkBinding<Color> binding in m_BackgroundFillColorBindings) binding.Sync();
        });
        TextColorProperty = new(() => m_TextColor, value =>
        {
            m_TextColor = value;
            foreach(DkBinding<Color> binding in m_TextColorBindings) binding.Sync();
        });
        ThumbColorProperty = new(() => m_ThumbColor, value =>
        {
            m_ThumbColor = value;
            foreach(DkBinding<Color> binding in m_ThumbColorBindings) binding.Sync();
        });
        ThumbOutlineColorProperty = new(() => m_ThumbOutlineColor, value =>
        {
            m_ThumbOutlineColor = value;
            foreach(DkBinding<Color> binding in m_ThumbOutlineColorBindings) binding.Sync();
        });
    }
    public EhSubTabOption              SubTabOption                { get; }      = new();
    public DkProperty<float>           AnimationSpeed              { get; set; } = new(0.7f);
    public float                       BackgroundBorder            { get; set; } = 90f;
    public int                         NameFontSize                { get; set; } = 14;
    public int                         ValueFontSize               { get; set; } = 10;
    public float                       SliderHeight                { get; set; } = 5;
    public float                       SliderThumbOutlineSize      { get; set; } = 16;
    public float                       SliderThumbSize             { get; set; } = 12;
    public float                       SliderWidth                 { get; set; } = 150;
    public float                       ThumbBorder                 { get; set; } = 90f;
    public TextAnchor                  NameAlignment               { get; set; } = TextAnchor.MiddleLeft;
    public TextAnchor                  ValueAlignment              { get; set; } = TextAnchor.UpperRight;
    public DkScriptableProperty<Color> BackgroundColorProperty     { get; }
    public DkScriptableProperty<Color> BackgroundFillColorProperty { get; }
    public DkScriptableProperty<Color> TextColorProperty           { get; }
    public DkScriptableProperty<Color> ThumbColorProperty          { get; }
    public DkScriptableProperty<Color> ThumbOutlineColorProperty   { get; }
}