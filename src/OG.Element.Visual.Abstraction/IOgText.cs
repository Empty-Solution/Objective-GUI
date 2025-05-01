using OG.Event.Abstraction.Handlers;
using OG.Graphics.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgText : IOgVisual<IOgTextRepaintEvent, OgTextRepaintContext>, IOgTextRepaintEventHandler;