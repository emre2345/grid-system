using System.Collections.Generic;
using System.Linq;
using DH.GridSystem.Cell;
using DH.GridSystem.Configuration;
using DH.GridSystem.Visitors;

namespace DH.GridSystem.Grids
{
    public class SquareGrid : IGrid
    {
        private CellCollection allCells;
        private List<CellCollection> columns;
        private List<CellCollection> rows;

        public List<CellCollection> Columns
        {
            get { return columns; }
        }

        public List<CellCollection> Rows
        {
            get { return rows; }
        }

        private int columnCount;

        public Cell.Cell[] CellsWithContent
        {
            get { return allCells.CellsWithContent.ToArray(); }
        }

        public Cell.Cell[] EmptyCells => allCells.EmptyCells.ToArray();

        public SquareGrid(IGridConfiguration config)
        {
            columnCount = config.ColumnCount;

            CreateCells(config.ColumnCount, config.RowCount);
            BuildColumns(config.ColumnCount, config.RowCount);
            BuildRows(config.ColumnCount, config.RowCount);
        }

        void CreateCells(int columnCount, int rowCount)
        {
            allCells = new CellCollection(Enumerable.Range(0, columnCount * rowCount).Select(delegate(int index)
            {
                int column, row;
                GridMath.CalculateColumnRow(index, columnCount, out column, out row);
                return new Cell.Cell(column, row);
            }));
        }

        void BuildColumns(int columnCount, int rowCount)
        {
            columns = new List<CellCollection>(Enumerable.Range(0, columnCount).Select<int, CellCollection>(
                delegate(int columnIndex)
                {
                    List<Cell.Cell> columnCells = Enumerable.Range(0, rowCount)
                        .Select<int, Cell.Cell>(delegate(int rowIndex) { return GetCell(columnIndex, rowIndex); })
                        .ToList();

                    CellCollection column = new CellCollection(columnCells);
                    return column;
                }));
        }

        void BuildRows(int columnCount, int rowCount)
        {
            rows = new List<CellCollection>(Enumerable.Range(0, rowCount).Select<int, CellCollection>(
                delegate(int rowIndex)
                {
                    List<Cell.Cell> rowCells = Enumerable.Range(0, columnCount)
                        .Select<int, Cell.Cell>(delegate(int columnIndex) { return GetCell(columnIndex, rowIndex); })
                        .ToList();

                    CellCollection row = new CellCollection(rowCells);
                    return row;
                }));
        }

        public Cell.Cell GetCell(int column, int row)
        {
            return allCells[column + columnCount * row];
        }

        public void Accept(IGridVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (obj is SquareGrid)
            {
                SquareGrid other = obj as SquareGrid;

                if (other.allCells.Count != allCells.Count)
                    return false;

                for (int i = 0; i < other.allCells.Count; i++)
                {
                    if (!other.allCells[i].Content.Equals(allCells[i].Content))
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}