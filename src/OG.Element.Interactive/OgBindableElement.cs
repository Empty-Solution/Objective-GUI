using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
namespace OG.Element.Interactive;
public class OgBindableElement<TElement, TValue> : OgFocusableElement<TElement, TValue>, IOgBindableElement<TElement, TValue>,
                                                   IOgEventCallback<IOgKeyBoardKeyDownEvent>,
                                                   IOgEventCallback<IOgKeyBoardKeyUpEvent> where TElement : IOgElement
{
    private readonly IDkProperty<KeyCode>     m_Bind;
    private readonly IDkValueOverride<TValue> m_Override;
    private          bool                     m_IsCapturing;
    public OgBindableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<TValue> value,
        IDkValueOverride<TValue> valueOverride, IDkProperty<KeyCode> bind) : base(name, provider, rectGetter, value)
    {
        m_Bind     = bind;
        m_Override = valueOverride;
        provider.Register<IOgKeyBoardKeyDownEvent>(this);
        provider.Register<IOgKeyBoardKeyUpEvent>(this);
    }
    public IDkObservable<TValue>? BindObservable { get; set; }
    public bool Invoke(IOgKeyBoardKeyDownEvent reason)
    {
        if(m_IsCapturing) return Bind(reason);
        return reason.KeyCode == m_Bind.Get() && Override();
    }
    public bool Invoke(IOgKeyBoardKeyUpEvent reason) => !m_IsCapturing;
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        m_Bind.Set(KeyCode.None);
        m_IsCapturing = true;
        return true;
    }
    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason)
    {
        m_IsCapturing = false;
        return true;
    }
    private bool Bind(IOgKeyBoardKeyDownEvent reason)
    {
        if(reason.KeyCode == KeyCode.Escape)
        {
            m_Bind.Set(KeyCode.None);
            IsFocusing = false;
            return true;
        }
        m_Bind.Set(reason.KeyCode);
        IsFocusing    = false;
        m_IsCapturing = false;
        return true;
    }
    private bool Override()
    {
        if(m_Override.IsOverriden)
            m_Override.Revert(Name);
        else
            m_Override.Override(Name, Value);
        return false;
    }
}