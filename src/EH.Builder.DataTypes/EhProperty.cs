using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction;
using DK.Observing.Abstraction;
using DK.Observing.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.DataTypes;
public class EhProperty<TValue> : IEhProperty<TValue>
{
    private readonly List<IDkProperty<KeyCode>> m_Keybinds;
    private readonly IDkObservable<TValue>      m_Observable;
    private          TValue                     m_Value;
    public EhProperty(IDkObservable<TValue> observable, TValue initial)
    {
        m_Observable  = observable;
        m_Value       = initial;
        ValueOverride = new EhValueOverride<TValue>(new Dictionary<object?, IDkGetProvider<TValue>>(), this);
        m_Keybinds    = [];
    }
    public IEhValueOverride<TValue>          ValueOverride { get; }
    public IEnumerable<IDkProperty<KeyCode>> Keybinds      => m_Keybinds;
    public TValue Get() => IsOverriden ? ValueOverride.Get() : m_Value;
    object IDkGetProvider.Get() => Get()!;
    public bool Set(TValue value)
    {
        if(IsOverriden) return true;
        m_Value = value;
        Notify(value);
        return true;
    }
    public bool Set(object value)
    {
        if(value is TValue typedValue) return Set(typedValue);
        return false;
    }
    public bool IsOverriden       => ValueOverride.IsOverriden;
    public Type InType            => typeof(TValue);
    public Type OutType           => typeof(TValue);
    public bool HasInternalSetter => false;
    public bool HasInternalGetter => false;
    public bool IsReadOnly        => IsOverriden;
    public bool IsWriteOnly       => false;
    public TValue GetOriginal() => m_Value;
    object IDkOriginalProvider.GetOriginal() => GetOriginal()!;
    public void AddObserver(IDkObserver observer) => m_Observable.AddObserver(observer);
    public void RemoveObserver(IDkObserver observer) => m_Observable.RemoveObserver(observer);
    public void AddObserver(IDkObserver<TValue> observer) => m_Observable.AddObserver(observer);
    public void RemoveObserver(IDkObserver<TValue> observer) => m_Observable.RemoveObserver(observer);
    public void Notify(TValue state) => m_Observable.Notify(m_Value);
    public IDkProperty<KeyCode> CreateKeybind(KeyCode keyCode)
    {
        DkProperty<KeyCode> keybind = new(keyCode);
        m_Keybinds.Add(keybind);
        return keybind;
    }
    public void RemoveKeybind(IDkProperty<KeyCode> keybind) => m_Keybinds.Remove(keybind);
}