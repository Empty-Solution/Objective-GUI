namespace OG.Event.Abstraction.Handlers;

public interface IOgRepaintEventHandler
{
    bool HandleRepaint(IOgRepaintEvent reason);
}