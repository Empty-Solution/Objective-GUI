namespace OG.Event.Abstraction.Handlers;

public interface IOgMouseScrollEventHandler
{
    bool HandleMouseScroll(IOgMouseScrollEvent reason);
}