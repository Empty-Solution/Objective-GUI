using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
namespace OG.DataKit.Animation;
public abstract class OgAnimationGetter<TValue> : IDkGetProvider<TValue>, IOgEventCallback<IOgRenderEvent> where TValue : notnull
{
    protected readonly IOgAnimator<TValue>    m_Animator;
    protected readonly IDkGetProvider<TValue> m_OriginalGetter;
    protected OgAnimationGetter(IDkGetProvider<TValue> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<TValue> animator)
    {
        m_OriginalGetter = originalGetter;
        m_Animator       = animator;
        provider.ForceRegister(this);
    }
    public IOgEventCallback<IOgRenderEvent>? RenderCallback { get; set; }
    public TValue Get() => InternalGet(m_OriginalGetter.Get(), m_Animator.Value);
    object IDkGetProvider.Get() => Get();
    public bool Invoke(IOgRenderEvent reason)
    {
        m_Animator.Animate(reason.DeltaTime, GetTarget());
        RenderCallback!.Invoke(reason);
        return false;
    }
    protected abstract TValue InternalGet(TValue originalValue, TValue animationValue);
    protected abstract TValue GetTarget();
}