using System.Collections.Generic;
using DH.GridSystem.Cell;
using DH.GridSystem.Grids;
using UnityEngine;

namespace DH.GridSystem.Visitors
{
    public class ColumnUpShifter : IGridVisitor
    {
        private int column;
        private int shiftAmount;

        public ColumnUpShifter(int column, int shiftAmount)
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
            
            for (int i = column.Count - 1; i >= 0; i--)
            {
                int newIndex = i - shiftAmount;
                if (newIndex < 0)
                    newIndex = column.Count - Mathf.Abs(newIndex) % column.Count;
                
                column[newIndex].Content = contents[i];
            }
        }
    }
}