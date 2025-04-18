using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Graphics.Abstraction;

public interface IOgTextureGraphicsContext<TStyle> : IOgStyledGraphicsContext<Texture, TStyle> where TStyle : IOgTextureStyle;