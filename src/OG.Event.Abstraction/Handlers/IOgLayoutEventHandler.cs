namespace OG.Event.Abstraction.Handlers;

public interface IOgLayoutEventHandler
{
    bool HandleLayout(IOgLayoutEvent reason);
}