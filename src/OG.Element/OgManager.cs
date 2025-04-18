using OG.Common.Abstraction;
using OG.Element.Abstraction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OG.Element;

public abstract class OgManager : MonoBehaviour
{
    private const string SEPARATOR = ".";
    private readonly Dictionary<string, IOgElement> m_ElementsCache = [];
    private Vector2 m_PrevMousePosition;
    public abstract IOgContainer<IOgElement> Root { get; }

    protected virtual void OnGUI()
    {
        OgEvent reason = GetReason(Event.current);
        UpdateMousePosition(reason);
        ProcessEvent(reason);
    }

    protected virtual OgEvent GetReason(Event uEvent) => new(uEvent, m_PrevMousePosition, GUI.matrix);

    protected virtual void UpdateMousePosition(OgEvent reason) => m_PrevMousePosition = reason.MousePosition;

    protected virtual void ProcessEvent(OgEvent reason) => Root.OnGUI(reason);

    public IOgElement? FindElement(string elementPath) =>
        m_ElementsCache.TryGetValue(elementPath, out IOgElement? element) ? element : RecursiveSearchWithCache(elementPath, Root);

    private IOgElement? RecursiveSearchWithCache(string elementPath, IOgContainer<IOgElement> container, string accumulatedPath = "")
    {
        string[] split = elementPath.Split([SEPARATOR], 2, StringSplitOptions.None);
        string currentName = split[0];
        string? remainingPath = split.Length > 1 ? split[1] : null;

        if(!string.IsNullOrEmpty(accumulatedPath))
            accumulatedPath += $"{SEPARATOR}{currentName}";
        else
            accumulatedPath = currentName;

        foreach(IOgElement? child in container.Children)
        {
            string childFullPath = $"{accumulatedPath}{SEPARATOR}{child.Name}";
            if(!m_ElementsCache.ContainsKey(childFullPath))
                m_ElementsCache.Add(childFullPath, child);

            if(child.Name != currentName)
                continue;

            if(remainingPath == null)
                return child;

            if(child is not IOgContainer<IOgElement> childContainer)
                continue;

            return RecursiveSearchWithCache(remainingPath, childContainer, accumulatedPath);
        }

        return null;
    }
}