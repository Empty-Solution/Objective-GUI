using DK.Observing.Abstraction.Generic;
namespace OG.DataKit.Animation.Observer;
public abstract class OgAnimationObserver<TObserverValue> : IDkObserver<TObserverValue>
{
    public void Update(TObserverValue state) => InternalUpdate(state);
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
    protected abstract void InternalUpdate(TObserverValue state);
}