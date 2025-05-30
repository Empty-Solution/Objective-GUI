using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Overriding.Abstraction.Generic;
namespace EH.Builder.DataTypes;
public interface IEhProperty<TValue> : IDkOverridableProperty<TValue>, IDkObservableProperty<TValue>
{
    void Notify(TValue state);
    IEhValueOverride<TValue> ValueOverride { get; }
}