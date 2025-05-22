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
    public float                       WindowWidth             { get; set; } = 640;
    public float                       WindowHeight            { get; set; } = 480;
    public float                       WindowBorder            { get; set; } = 15;
    public DkScriptableProperty<Color> BackgroundColorProperty { get; }
}