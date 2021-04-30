using System.Collections.Generic;
using UnityEngine;

namespace DH.Grid.Visitors
{
    public class RowLeftShifter : IGridVisitor
    {
        private int row;
        private int shiftAmount;

        public RowLeftShifter(int row, int shiftAmount)
        {
            this.row = row;
            this.shiftAmount = shiftAmount;
        }

        public void Visit(DH.Grid.SquareGrid g)
        {
            Dictionary<int, ICellContent> contents = new Dictionary<int, ICellContent>(g.Rows[this.row].Count);
            CellCollection row = g.Rows[this.row];
            for (int i = 0; i < row.Count; i++)
            {
                contents.Add(i, row[i].Content);
            }
            
            for (int i = row.Count - 1; i >= 0; i--)
            {
                int newIndex = i - shiftAmount;
                if (newIndex < 0)
                    newIndex = row.Count - Mathf.Abs(newIndex) % row.Count;
                
                row[newIndex].Content = contents[i];
            }
        }
    }
}