using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using DH.Grid.Configuration;
using DH.Grid;
using DH.Grid.Visitors;
using Random = System.Random;

public class Grid
{
    IGridConfiguration CreateTestConfig()
    {
        IGridConfiguration conf = Substitute.For<IGridConfiguration>();
        conf.ColumnCount.Returns(5);
        conf.RowCount.Returns(5);
        return conf;
    }

    [Test]
    public void CalculateColumnRow()
    {
        int column = 5;
        int row = 10;
        int total = column * row;

        int calcColumn;
        int calcRow;

        int currentColumn = 0;
        int currentRow = 0;

        for (int i = 0; i < total; i++)
        {
            GridMath.CalculateColumnRow(i, column, row, out calcColumn, out calcRow);
            Assert.AreEqual(currentColumn, calcColumn, string.Format("Column calculation wrong - cell: {0}", i));
            Assert.AreEqual(currentRow, calcRow, string.Format("Row calculation wrong - cell: {0}", i));
            if (++currentColumn >= column)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }

    [Test]
    public void BasicGridCreation()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        Assert.AreEqual(conf.ColumnCount, grid.Columns.Count);
        Assert.AreEqual(conf.RowCount, grid.Rows.Count);

        foreach (CellCollection column in grid.Columns)
        {
            for (int i = 0; i < column.Count; i++)
            {
                Assert.IsNotNull(column[i]);
                Assert.AreEqual(i, column[i].Row);
            }
        }

        foreach (CellCollection row in grid.Rows)
        {
            for (int i = 0; i < row.Count; i++)
            {
                Assert.IsNotNull(row[i]);
                Assert.AreEqual(i, row[i].Column);
            }
        }
    }

    [Test]
    public void SetGridCellContent()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        ICellContent content = Substitute.For<ICellContent>();
        Cell cell = grid.GetCell(0, 0);
        cell.Content = content;

        Assert.AreEqual(content, grid.GetCell(0, 0).Content);
    }

    [Test]
    public void ShiftRowRight()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        List<ICellContent> contents = new List<ICellContent>(conf.ColumnCount * conf.RowCount);
        FillRowContents(grid, contents);

        Assert.AreSame(contents[0], grid.Rows[0][0].Content);
        Assert.AreSame(contents[1], grid.Rows[0][1].Content);
        ICellContent firstColumnContent = grid.Rows[0][0].Content;

        grid.Accept(new RowRightShifter(0, 3));

        Assert.AreSame(firstColumnContent, grid.Rows[0][3].Content);

        grid.Accept(new RowRightShifter(0, 10));

        Assert.AreSame(firstColumnContent, grid.Rows[0][3].Content);

        grid.Accept(new RowRightShifter(0, 13));

        Assert.AreSame(firstColumnContent, grid.Rows[0][1].Content);
    }

    void FillRowContents(IGrid grid, List<ICellContent> contents)
    {
        foreach (CellCollection row in grid.Rows)
        {
            row.Foreach(delegate(Cell c)
            {
                ICellContent content = Substitute.For<ICellContent>();
                c.Content = content;
                contents.Add(content);
            });
        }
    }

    [Test]
    public void ShiftRowLeft()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        List<ICellContent> contents = new List<ICellContent>(conf.ColumnCount * conf.RowCount);
        FillRowContents(grid, contents);

        Assert.AreSame(contents[0], grid.Rows[0][0].Content);
        Assert.AreSame(contents[1], grid.Rows[0][1].Content);
        ICellContent firstColumnContent = grid.Rows[0][0].Content;

        grid.Accept(new RowLeftShifter(0, 2));

        Assert.AreSame(firstColumnContent, grid.Rows[0][3].Content);
    }

    [Test]
    public void ShiftColumnUp()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        List<ICellContent> contents = new List<ICellContent>(conf.ColumnCount * conf.RowCount);
        FillColumnContents(grid, contents);

        Assert.AreSame(contents[0], grid.Columns[0][0].Content);
        Assert.AreSame(contents[1], grid.Columns[0][1].Content);
        ICellContent firstRowContent = grid.Columns[0][0].Content;

        grid.Accept(new ColumnUpShifter(0, 2));

        Assert.AreSame(firstRowContent, grid.Columns[0][3].Content);
    }
    
    [Test]
    public void ShiftColumnDown()
    {
        IGridConfiguration conf = CreateTestConfig();
        IGrid grid = new DH.Grid.SquareGrid(conf);

        List<ICellContent> contents = new List<ICellContent>(conf.ColumnCount * conf.RowCount);
        FillColumnContents(grid, contents);

        Assert.AreSame(contents[0], grid.Columns[0][0].Content);
        Assert.AreSame(contents[1], grid.Columns[0][1].Content);
        ICellContent firstRowContent = grid.Columns[0][0].Content;

        grid.Accept(new ColumnDownShifter(0, 3));

        Assert.AreSame(firstRowContent, grid.Columns[0][3].Content);

        grid.Accept(new ColumnDownShifter(0, 10));

        Assert.AreSame(firstRowContent, grid.Columns[0][3].Content);

        grid.Accept(new ColumnDownShifter(0, 13));

        Assert.AreSame(firstRowContent, grid.Columns[0][1].Content);
    }

    void FillColumnContents(IGrid grid, List<ICellContent> contents)
    {
        foreach (CellCollection column in grid.Columns)
        {
            column.Foreach(delegate(Cell c)
            {
                ICellContent content = Substitute.For<ICellContent>();
                c.Content = content;
                contents.Add(content);
            });
        }
    }
}