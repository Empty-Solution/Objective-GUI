using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy.Tools;

internal class InternalLegacyGuiStyleTool
{
    private static readonly GUIStyle tempStyle = new();

    internal static GUIStyle GetUnityStyle(IOgUnityStyleFiller filler)
    {
        filler.FillUnityStyle(tempStyle);
        return tempStyle;
    }
}