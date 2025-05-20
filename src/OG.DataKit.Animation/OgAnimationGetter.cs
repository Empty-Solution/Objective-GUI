using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public abstract class OgAnimationGetter<TGetter, TValue> : IDkGetProvider<TValue>, IOgEventCallback<IOgRenderEvent>
    where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    private float   m_Time;
    private TValue? m_Value;
    protected OgAnimationGetter(TGetter originalGetter, IOgEventHandlerProvider provider)
    {
        OriginalGetter = originalGetter;
        provider.Register(this);
    }
    public TValue?                           TargetModifier { protected get; set; }
    public IOgEventCallback<IOgRenderEvent>? RenderCallback { get;           set; }
    public TGetter                           OriginalGetter { get; }
    public float                             Speed          { get; set; } = 1f;
    public TValue Get() => m_Value = CalculateValue(m_Value!, CalculateValue(OriginalGetter.Get(), TargetModifier!), m_Time);
    object IDkGetProvider.Get() => Get();
    public bool Invoke(IOgRenderEvent reason)
    {
        m_Time = Mathf.Clamp01(m_Time + (reason.DeltaTime * Speed));
        RenderCallback!.Invoke(reason);
        return false;
    }
    public void SetTime(float time = 0f) => m_Time = time;
    protected abstract TValue CalculateValue(TValue currentValue, TValue targetValue, float time);
    protected abstract TValue CalculateValue(TValue originalValue, TValue targetModifier);
}