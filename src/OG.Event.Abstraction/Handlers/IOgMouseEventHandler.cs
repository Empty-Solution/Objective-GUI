namespace OG.Event.Abstraction.Handlers;

public interface IOgMouseEventHandler
{
    bool ProcElementsBackward(IOgInputEvent reason);
    bool ProcElementsForward(IOgEvent reason);
}