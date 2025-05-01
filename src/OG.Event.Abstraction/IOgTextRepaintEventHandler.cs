using OG.Event.Abstraction.Handlers;

namespace OG.Event.Abstraction;

public interface IOgTextRepaintEventHandler
{
    OgTextRepaintContext HandleTextRepaint(IOgTextRepaintEvent reason);
}