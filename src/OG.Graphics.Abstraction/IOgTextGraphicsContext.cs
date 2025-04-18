using OG.Style.Abstraction;

namespace OG.Graphics.Abstraction;

public interface IOgTextGraphicsContext<TStyle> : IOgStyledGraphicsContext<string, TStyle> where TStyle : IOgTextStyle;