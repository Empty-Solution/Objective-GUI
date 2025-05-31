using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction;
using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Overriding.Abstraction;
using DK.Property.Overriding.Abstraction.Generic;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.DataTypes;
public interface IEhProperty<TValue> : IEhProperty, IDkOverridableProperty<TValue>, IDkObservableProperty<TValue>
{
    IEhValueOverride<TValue>          ValueOverride { get; }
    IEnumerable<IDkProperty<KeyCode>> Keybinds      { get; }
    void Notify(TValue state);
    public IDkProperty<KeyCode> CreateKeybind(KeyCode keyCode);
    public void RemoveKeybind(IDkProperty<KeyCode> keybind);
}
public interface IEhProperty : IDkOverridableProperty, IDkObservableProperty;