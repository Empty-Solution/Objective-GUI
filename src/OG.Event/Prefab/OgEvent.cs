using OG.Event.Abstraction;
namespace OG.Event.Prefab;
public abstract class OgEvent : IOgEvent
{
    public          bool IsConsumed { get; private set; }
    public virtual  void Consume()  => IsConsumed = true;
    public virtual  void Reset()    => IsConsumed = false;
}