using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.DataKit.Static;
public class OgReadOnlyRectGetter(Rect rect) : IDkGetProvider<Rect>
{
    protected Rect m_Rect = rect;
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
}