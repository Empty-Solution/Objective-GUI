using System;
namespace OG.Event.Prefab;
public abstract class OgUnconsumeEvent : OgEvent
{
    public override void Consume() => throw new InvalidOperationException();
}