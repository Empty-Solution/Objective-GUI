namespace OG.Event.Abstraction.Handlers;

public interface IOgMouseMoveEventHandler
{
    bool HandleMouseMove(IOgMouseMoveEvent reason);
}