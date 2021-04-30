using System.Collections.Generic;
using DH.Grid.Visitors;

namespace DH.Grid
{
    public interface IGrid
    {
        List<CellCollection> Columns { get; }
        List<CellCollection> Rows { get; }

        Cell GetCell(int column, int row);

        void Accept(IGridVisitor visitor);
    }
}