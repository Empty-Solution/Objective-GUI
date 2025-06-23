using DK.Setting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.DataKit.Transformer;
public class OgTransformerRectField(IOgEventHandlerProvider provider, IOgOptionsContainer options)
    : OgTransformerRectGetter(provider, options), IDkSetProvider<Rect>
{
    protected Rect m_Modifier = Rect.zero;
    public bool Set(Rect value)
    {
        if(m_Rect.Equals(value)) return false;
        m_Modifier.position += m_Rect.position - value.position;
        m_Modifier.size += m_Rect.size - value.size;
        return true;
    }
    public bool Set(object value) => value is Rect rect && Set(rect);
    public override bool Invoke(IOgLayoutEvent reason)
    {
        _ = base.Invoke(reason);
        if(m_Modifier == Rect.zero) return false;
        m_Rect.position += m_Modifier.position;
        m_Rect.size += m_Modifier.size;
        return false;
    }
}