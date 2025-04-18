using UnityEngine;

namespace OG.Style.Abstraction;

public interface IOgTextStyle : IOgColorizeStyle, IOgUnityStyleFiller
{
    Font Font { get; set; }

    FontStyle FontStyle { get; set; }

    bool WordWrap { get; set; }

    int FontSize { get; set; }

    TextAnchor Alignment { get; set; }

    TextClipping Clipping { get; set; }
}