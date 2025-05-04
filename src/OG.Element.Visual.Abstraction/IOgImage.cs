using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Texture;
using OG.Event.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgImage : IOgVisual
{
    IDkGetProvider<IOgTexture>? Texture { get; set; }
}

