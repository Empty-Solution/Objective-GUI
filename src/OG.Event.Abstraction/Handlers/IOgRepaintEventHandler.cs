namespace OG.Event.Abstraction.Handlers;

public interface IOgRepaintEventHandler<TEvent, TReturn> where TEvent : IOgRepaintEvent
{
    TReturn HandleRepaint(TEvent reason);
}