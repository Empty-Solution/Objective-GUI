using DK.Setting.Abstraction.Generic;
using UnityEngine;
namespace OG.DataKit.Static;
public class OgReadOnlyRectField(Rect rect) : OgReadOnlyRectGetter(rect), IDkSetProvider<Rect>
{
    public bool Set(Rect value)
    {
        if(m_Rect.Equals(value)) return false;
        m_Rect = value;
        return true;
    }
    public bool Set(object value)
    {
        if(value is Rect rect) return Set(rect);
        return false;
    }
}