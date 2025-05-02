namespace OG.Event.Abstraction.Handlers;
public interface IOgRepaintEventHandler<in TEvent, out TReturn> where TEvent : IOgRepaintEvent
{
    TReturn HandleRepaint(TEvent reason);
}