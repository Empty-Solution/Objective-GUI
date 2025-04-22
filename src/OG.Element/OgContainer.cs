using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OG.Element;

public class OgContainer<TElement, TScope>(string name, TScope scope, IOgTransform transform)
    : OgElement<TScope>(name, scope, transform), IOgContainer<TElement> where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgElementAddedHandler(OgContainer<TElement, TScope> instance, IOgElement element);

    public delegate void OgElementRemovedHandler(OgContainer<TElement, TScope> instance, IOgElement element, int index);

    private readonly List<TElement> m_Children = [];
    internal IReadOnlyList<TElement>? m_ReadOnlyChildren;

    public IReadOnlyList<TElement> Children => m_ReadOnlyChildren ??= m_Children.AsReadOnly();

    IEnumerable<IOgElement> IOgContainer.Children => Children.Cast<IOgElement>();

    public virtual void AddChild(TElement child)
    {
        if(ContainsChild(child.Name)) throw new InvalidOperationException();
        InternalAddChild(child);
    }

    public virtual void RemoveChild(TElement child)
    {
        int index = m_Children.IndexOf(child);
        if(index < 0) throw new InvalidOperationException();
        InternalRemoveChild(child, index);
    }

    public bool ContainsChild(string name)
    {
        foreach(TElement c in m_Children)
            if(c.Name == name)
                return true;
        return false;
    }

    public virtual void AddChild(IOgElement child)
    {
        if(child is not TElement castedElement) throw new InvalidCastException();
        AddChild(castedElement);
    }

    public virtual void RemoveChild(IOgElement child)
    {
        if(child is not TElement castedElement) throw new InvalidCastException();
        RemoveChild(castedElement);
    }

    public event OgElementRemovedHandler? OnElementRemoved;
    public event OgElementAddedHandler? OnElementAdded;

    public bool ContainsChild(IOgElement child) => child is TElement castedElement ? ContainsChild(castedElement) : throw new InvalidCastException();

    protected override void InternalOnGUI(OgEvent reason) => ProcessChildren(reason);

    protected virtual void InternalAddChild(TElement child)
    {
        m_Children.Add(child);
        OnElementAdded?.Invoke(this, child);
    }

    protected virtual void InternalRemoveChild(TElement child, int index)
    {
        m_Children.RemoveAt(index);
        OnElementRemoved?.Invoke(this, child, index);
    }

    protected virtual void ProcessChildren(OgEvent reason)
    {
        if(reason.Type is EventType.Repaint or EventType.Layout)
        {
            ProcessChildrenForward(reason);
            return;
        }

        ProcessChildrenBackward(reason);
    }

    private void ProcessChildrenForward(OgEvent reason)
    {
        for(int i = 0; i < m_Children.Count; i++) ProcessChild(reason, m_Children[i]);
    }

    private void ProcessChildrenBackward(OgEvent reason)
    {
        for(int i = m_Children.Count - 1; i >= 0; i--) ProcessChild(reason, m_Children[i]);
    }

    protected virtual void ProcessChild(OgEvent reason, TElement child) => child.OnGUI(reason);
}