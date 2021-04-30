using System.Collections.Generic;
using DH.GridSystem.Cell;
using DH.GridSystem.Visitors;

namespace DH.GridSystem
{
    public interface IGrid
    {
        List<CellCollection> Columns
        {
            get;
        }

        List<CellCollection> Rows
        {
            get;
        }

        Cell.Cell GetCell(int column, int row);

        Cell.Cell[] CellsWithContent
        {
            get;
        }

        void Accept(IGridVisitor visitor);
    }
}