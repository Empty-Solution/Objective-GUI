using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using OG.Graphics;
namespace OG.Element.Visual.Abstraction;
public interface IOgText : IOgVisual<IOgTextRepaintEvent, OgTextRepaintContext>, IOgTextRepaintEventHandler;