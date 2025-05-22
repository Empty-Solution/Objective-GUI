using DK.Binding.Generic;
using DK.Property.Generic;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhToggleOption
{
    public readonly List<DkBinding<Color>> m_BackgroundColorBindings     = [];
    public readonly List<DkBinding<Color>> m_BackgroundFillColorBindings = [];
    public readonly List<DkBinding<Color>> m_TextColorBindings           = [];
    public readonly List<DkBinding<Color>> m_ThumbColorBindings          = [];
    private         Color                  m_BackgroundColor             = new Color32(30, 30, 30, 255);
    private         Color                  m_BackgroundFillColor         = new Color32(200, 200, 200, 200);
    private         Color                  m_TextColor                   = Color.white;
    private         Color                  m_ThumbColor                  = Color.white;
    public EhToggleOption()
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
    }
    public EhSubTabOption              SubTabOption                { get; }      = new();
    public DkProperty<float>           AnimationSpeed              { get; set; } = new(0.7f);
    public float                       BackgroundBorder            { get; set; } = 90f;
    public int                         FontSize                    { get; set; } = 14;
    public float                       ToggleHeight                { get; set; } = 22;
    public float                       ToggleWidth                 { get; set; } = 44;
    public float                       ThumbSize                   { get; set; } = 16;
    public float                       ThumbBorder                 { get; set; } = 90f;
    public TextAnchor                  NameAlignment               { get; set; } = TextAnchor.MiddleLeft;
    public DkScriptableProperty<Color> BackgroundColorProperty     { get; }
    public DkScriptableProperty<Color> BackgroundFillColorProperty { get; }
    public DkScriptableProperty<Color> TextColorProperty           { get; }
    public DkScriptableProperty<Color> ThumbColorProperty          { get; }
}