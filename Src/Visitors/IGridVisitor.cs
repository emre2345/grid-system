using DH.GridSystem.Grids;

namespace DH.GridSystem.Visitors
{
    public interface IGridVisitor
    {
        void Visit(IGrid g);
    }
}