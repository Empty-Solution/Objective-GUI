using DK.Observing.Abstraction.Generic;
using EH.Builder.Wrapping.DataTypes;
using System.Linq;
namespace EH.Builder.Wrapping;
public class EhSubTabObserver(EhSourceTab tab) : IDkObserver<int>
{
    public void Update(int state)
    {
        tab.SourceContainer.Clear();
        tab.SourceContainer.Add(tab.SubTabs.ElementAt(state).SourceContainer);
    }
    public void Update(object state)
    {
        if(state is int value) Update(value);
    }
}