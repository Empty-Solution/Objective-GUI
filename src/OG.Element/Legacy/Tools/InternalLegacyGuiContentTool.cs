using UnityEngine;

namespace OG.Element.Legacy.Tools;

internal class InternalLegacyGuiContentTool
{
    private static readonly GUIContent tempContent = new();

    internal static GUIContent GetContent(string str)
    {
        tempContent.text = str;
        return tempContent;
    }
}