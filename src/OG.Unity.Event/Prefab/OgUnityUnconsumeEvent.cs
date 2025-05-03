using System;
namespace OG.Unity.Event.Prefab;
public abstract class OgUnityUnconsumeEvent : OgUnityEvent
{
    public override void Consume() => throw new InvalidOperationException();
}