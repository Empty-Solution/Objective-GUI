#region

using OG.Event.Abstraction;

#endregion

namespace OG.Unity.Event.Prefab;

public class OgUnityEvent : IOgEvent
{
    public bool IsConsumed { get; private set; }

    public virtual void Consume() => IsConsumed = true;

    public virtual void Reset() => IsConsumed = false;
}