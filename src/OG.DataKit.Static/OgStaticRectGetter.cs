using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.DataKit.Transformer;
public class OgStaticRectGetter(Rect rect) : IDkGetProvider<Rect>
{
    protected Rect m_Rect = rect;
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
}