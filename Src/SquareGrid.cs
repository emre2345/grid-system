using System.Collections.Generic;
using System.Linq;
using DH.Grid.Configuration;
using DH.Grid.Visitors;
using UnityEngine;

namespace DH.Grid
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
                GridMath.CalculateColumnRow(index, columnCount, rowCount, out column, out row);
                return new Cell(column, row);
            }));
        }

        void BuildColumns(int columnCount, int rowCount)
        {
            columns = new List<CellCollection>(Enumerable.Range(0, columnCount).Select<int, CellCollection>(
                delegate(int columnIndex)
                {
                    List<Cell> columnCells = Enumerable.Range(0, rowCount).Select<int, Cell>(delegate(int rowIndex)
                    {
                        return GetCell(columnIndex, rowIndex);
                    }).ToList();

                    CellCollection column = new CellCollection(columnCells);
                    return column;
                }));
        }

        void BuildRows(int columnCount, int rowCount)
        {
            rows = new List<CellCollection>(Enumerable.Range(0, rowCount).Select<int, CellCollection>(
                delegate(int rowIndex)
                {
                    List<Cell> rowCells = Enumerable.Range(0, columnCount).Select<int, Cell>(delegate(int columnIndex)
                    {
                        return GetCell(columnIndex, rowIndex);
                    }).ToList();

                    CellCollection row = new CellCollection(rowCells);
                    return row;
                }));
        }

        public Cell GetCell(int column, int row)
        {
            return allCells[column + columnCount * row];
        }

        public void Accept(IGridVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}