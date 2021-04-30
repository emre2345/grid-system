using System.Collections.Generic;
using DH.GridSystem.Cell;
using DH.GridSystem.Grids;

namespace DH.GridSystem.Visitors
{
    public class RowRightShifter : IGridVisitor
    {
        private int row;
        private int shiftAmount;

        public RowRightShifter(int row, int shiftAmount)
        {
            this.row = row;
            this.shiftAmount = shiftAmount;
        }

        public void Visit(SquareGrid g)
        {
            Dictionary<int, ICellContent> contents = new Dictionary<int, ICellContent>(g.Rows[this.row].Count);
            CellCollection row = g.Rows[this.row];
            for (int i = 0; i < row.Count; i++)
            {
                contents.Add(i, row[i].Content);
            }
            
            for (int i = 0; i < row.Count; i++)
            {
                row[(i + shiftAmount) % row.Count].Content = contents[i];
            }
        }
    }
}