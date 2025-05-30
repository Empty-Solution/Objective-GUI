using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction;
using DK.Observing.Abstraction;
using DK.Observing.Abstraction.Generic;
using System;
using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public class EhProperty<TValue> : IEhProperty<TValue>
{
    private readonly IDkObservable<TValue> m_Observable;
    private          TValue                m_Value;
    public EhProperty(IDkObservable<TValue> observable, TValue initial)
    {
        m_Observable  = observable;
        m_Value       = initial;
        ValueOverride = new EhValueOverride<TValue>(new Dictionary<object?, IDkGetProvider<TValue>>(), this);
    }
    public IEhValueOverride<TValue> ValueOverride { get; }
    public TValue Get() => IsOverriden ? ValueOverride.Get() : m_Value;
    object IDkGetProvider.Get() => Get()!;
    public bool Set(TValue value)
    {
        m_Value = value;
        m_Observable.Notify(value);
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
    public bool IsReadOnly        => false;
    public bool IsWriteOnly       => false;
    public TValue GetOriginal() => m_Value;
    object IDkOriginalProvider.GetOriginal() => GetOriginal()!;
    public void AddObserver(IDkObserver observer) => m_Observable.AddObserver(observer);
    public void RemoveObserver(IDkObserver observer) => m_Observable.RemoveObserver(observer);
    public void AddObserver(IDkObserver<TValue> observer) => m_Observable.AddObserver(observer);
    public void RemoveObserver(IDkObserver<TValue> observer) => m_Observable.RemoveObserver(observer);
    public void Notify(TValue state) => m_Observable.Notify(m_Value);
}