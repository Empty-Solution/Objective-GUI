using DK.Getting.Abstraction.Generic;
using System;
namespace OG.DataKit.Animation.Observer;
public class OgAnimationArbitraryScriptableObserver<TGetter, TValue, TObserverValue>(Action<OgAnimationGetter<TGetter, TValue>, TObserverValue> func)
    : OgAnimationGetterObserver<TGetter, TValue, TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    protected override void InternalUpdate(TObserverValue state) => func.Invoke(Getter!, state);
}