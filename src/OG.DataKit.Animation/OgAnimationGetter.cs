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
    public TValue?                           TargetModifier { get; set; }
    public IOgEventCallback<IOgRenderEvent>? RenderCallback { get; set; }
    public TGetter                           OriginalGetter { get; }
    public IDkGetProvider<float>?            Speed          { get; set; }
    public TValue Get() => m_Value = CalculateValue(m_Value!, AddValue(OriginalGetter.Get(), TargetModifier!), m_Time);
    object IDkGetProvider.Get() => Get();
    public bool Invoke(IOgRenderEvent reason)
    {
        m_Time = Mathf.Clamp01(m_Time + (reason.DeltaTime * Speed?.Get() ?? 1));
        RenderCallback!.Invoke(reason);
        return false;
    }
    public void SetTime(float time = 0f) => m_Time = Mathf.Clamp01(time);
    public float GetTime() => m_Time;
    protected abstract TValue CalculateValue(TValue currentValue, TValue targetValue, float time);
    protected abstract TValue AddValue(TValue originalValue, TValue targetModifier);
}