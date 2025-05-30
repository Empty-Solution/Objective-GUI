using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Overriding.Abstraction.Generic;
namespace EH.Builder.DataTypes;
public interface IEhProperty<TValue> : IDkOverridableProperty<TValue>, IDkObservableProperty<TValue>
{
    IEhValueOverride<TValue> ValueOverride { get; }
    void Notify(TValue state);
}