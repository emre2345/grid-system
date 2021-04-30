using System.Linq;
using DH.GridSystem.Cell;
using DH.GridSystem.Configuration;
using DH.GridSystem.Grids;
using UnityEngine;

namespace DH.GridSystem.Factories
{
    public interface IRandomContentProvider
    {
        IInstantiatableCellContent Get();
    }

    public class SquareGridWithRandomContent : IGridFactory
    {
        private IRandomContentProvider randomContentProvider;
        private IGridConfiguration gridConfig;

        public SquareGridWithRandomContent(IRandomContentProvider randomContentProvider, IGridConfiguration gridConfig)
        {
            this.randomContentProvider = randomContentProvider;
            this.gridConfig = gridConfig;
        }

        public IGrid Create()
        {
            SquareGrid grid = new SquareGrid(gridConfig);

            int cellCount = gridConfig.RowCount * gridConfig.ColumnCount;
            for (int i = 0; i < cellCount; i++)
            {
                int row, column;
                GridMath.CalculateColumnRow(i, gridConfig.ColumnCount, gridConfig.RowCount, out column, out row);
                grid.GetCell(column, row).Content = randomContentProvider.Get();
            }

            return grid;
        }
    }
}