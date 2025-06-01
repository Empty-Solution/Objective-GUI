using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public interface IEhSubTab : IEhElement
{
    public IEnumerable<IEhTabGroup> Groups { get; }
    void AddGroup(IEhTabGroup group);
    void RemoveGroup(IEhTabGroup group);
}