using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.DataTypes.BindType;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
namespace OG.Element.Interactive;
public class OgBindableElement<TElement, TValue> : OgFocusableElement<TElement, TValue>, IOgEventCallback<IOgKeyBoardKeyDownEvent>,
                                                   IOgEventCallback<IOgKeyBoardKeyUpEvent> where TElement : IOgElement
{
    private readonly IDkProperty<SortedSet<KeyCode>> m_Bind;
    private readonly IDkGetProvider<EOgBindType>     m_BindTypeGetProvider;
    private readonly IDkValueOverride<TValue>        m_Override;
    private readonly List<KeyCode>                   m_PressedKeys = [];
    public OgBindableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<TValue> value,
        IDkValueOverride<TValue> valueOverride, IDkProperty<SortedSet<KeyCode>> bind, IDkGetProvider<EOgBindType> bindTypeGetProvider) : base(name,
        provider, rectGetter, value)
    {
        m_Bind                = bind;
        m_BindTypeGetProvider = bindTypeGetProvider;
        m_Override            = valueOverride;
        provider.Register<IOgKeyBoardKeyDownEvent>(this);
        provider.Register<IOgKeyBoardKeyUpEvent>(this);
    }
    public bool Invoke(IOgKeyBoardKeyDownEvent reason)
    {
        if(m_BindTypeGetProvider.Get() == EOgBindType.HOVER) m_Override.Revert(Name);
        if(IsFocusing) return Bind(reason);
        m_PressedKeys.Add(reason.KeyCode);
        return Override();
    }
    public bool Invoke(IOgKeyBoardKeyUpEvent reason)
    {
        if(IsFocusing) return false;
        m_PressedKeys.Remove(reason.KeyCode);
        return true;
    }
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        m_Bind.Get().Clear();
        return true;
    }
    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason) => true;
    private bool Bind(IOgKeyBoardKeyDownEvent reason)
    {
        if(reason.KeyCode == KeyCode.Escape)
        {
            m_Bind.Get().Clear();
            return true;
        }
        m_Bind.Get().Add(reason.KeyCode);
        return true;
    }
    private bool Override()
    {
        SortedSet<KeyCode> bindKeys = m_Bind.Get();
        int                matches  = m_PressedKeys.Count(key => bindKeys.Contains(key));
        if(matches != bindKeys.Count - 1) return false;
        if(m_BindTypeGetProvider.Get() == EOgBindType.TOGGLE && m_Override.IsOverriden)
            m_Override.Revert(Name);
        else
            m_Override.Override(Name, Value);
        return true;
    }
    public override bool ProcessEvent(IOgEvent reason) => (IsActive || reason is IOgKeyBoardKeyDownEvent) && base.ProcessEvent(reason);
}