using DK.Observing.Abstraction.Generic;
namespace OG.DataKit.Animation;
public class OgAnimationStateObserver<TValue>(OgAnimationStateGetter<TValue> getter, EOgAnimationState animationState) : IDkObserver<bool> where TValue : notnull
{
    public void Update(bool state) => getter.ChangeState(animationState);
    public void Update(object state)
    {
        if(state is bool casted) Update(casted);
    }
}