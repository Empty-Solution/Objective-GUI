using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using System;
namespace OG.DataKit.Animation.Observer;
public class OgAnimationScriptableObserver<TGetter, TValue, TObserverValue>(Func<OgAnimationGetter<TGetter, TValue>, TObserverValue, TValue> func)
    : OgAnimationGetterObserver<TGetter, TValue, TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    protected override void InternalUpdate(TObserverValue state) => Getter!.TargetModifier = func.Invoke(Getter, state);
}