using System;

namespace OG.Unity.Event.Prefab;

public class OgUnityUnconsumeEvent : OgUnityEvent
{
    public override void Consume() => throw new InvalidOperationException();
}