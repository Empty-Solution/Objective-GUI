using DK.Binding.Generic;
using DK.Property.Generic;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhSliderOption
{
    public readonly List<DkBinding<Color>> m_BackgroundColorBindings   = [];
    public readonly List<DkBinding<Color>> m_TextColorBindings         = [];
    public readonly List<DkBinding<Color>> m_ThumbColorBindings        = [];
    public readonly List<DkBinding<Color>> m_ThumbOutlineColorBindings = [];
    private         Color                  m_BackgroundColor           = Color.white;
    private         Color                  m_TextColor                 = Color.white;
    private         Color                  m_ThumbColor                = Color.black;
    private         Color                  m_ThumbOutlineColor         = Color.white;
    public EhSliderOption()
    {
        BackgroundColorProperty = new(() => m_BackgroundColor, value =>
        {
            m_BackgroundColor = value;
            foreach(DkBinding<Color> binding in m_BackgroundColorBindings) binding.Sync();
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
    public DkScriptableProperty<Color> BackgroundColorProperty   { get; }
    public DkScriptableProperty<Color> TextColorProperty         { get; }
    public DkScriptableProperty<Color> ThumbColorProperty        { get; }
    public DkScriptableProperty<Color> ThumbOutlineColorProperty { get; }
}