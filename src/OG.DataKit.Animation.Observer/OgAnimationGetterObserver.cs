using DK.Getting.Abstraction.Generic;
namespace OG.DataKit.Animation.Observer;
public abstract class OgAnimationGetterObserver<TGetter, TValue, TObserverValue> : OgAnimationObserver<TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    public OgAnimationGetter<TGetter, TValue>? Getter { get; set; }
}