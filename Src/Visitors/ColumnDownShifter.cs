using System.Collections.Generic;

namespace DH.Grid.Visitors
{
    public class ColumnDownShifter : IGridVisitor
    {
        private int column;
        private int shiftAmount;
        
        public ColumnDownShifter(int column, int shiftAmount)
        {
            this.column = column;
            this.shiftAmount = shiftAmount;
        }

        public void Visit(SquareGrid g)
        {
            Dictionary<int, ICellContent> contents = new Dictionary<int, ICellContent>(g.Columns[this.column].Count);
            CellCollection column = g.Columns[this.column];
            for (int i = 0; i < column.Count; i++)
            {
                contents.Add(i, column[i].Content);
            }
            
            for (int i = 0; i < column.Count; i++)
            {
                column[(i + shiftAmount) % column.Count].Content = contents[i];
            }
        }
    }
}