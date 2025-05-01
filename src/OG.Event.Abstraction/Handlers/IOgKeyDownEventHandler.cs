namespace OG.Event.Abstraction.Handlers;

public interface IOgKeyDownEventHandler
{
    bool HandleKeyDown(IOgKeyDownEvent reason);
}