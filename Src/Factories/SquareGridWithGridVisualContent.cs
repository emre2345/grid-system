using DH.GridSystem.Cell;
using DH.GridSystem.Configuration;
using DH.GridSystem.Grids;

namespace DH.GridSystem.Factories
{
    public class SquareGridWithGridVisualContent : IGridFactory
    {
        private IInstantiatableCellContent visualContent;
        private IGridConfiguration gridConfig;

        public SquareGridWithGridVisualContent(IInstantiatableCellContent visualContent, IGridConfiguration gridConfig)
        {
            this.visualContent = visualContent;
            this.gridConfig = gridConfig;
        }

        public IGrid Create()
        {
            SquareGrid grid = new SquareGrid(gridConfig);

            int cellCount = gridConfig.RowCount * gridConfig.ColumnCount;
            for (int i = 0; i < cellCount; i++)
            {
                int row, column;
                GridMath.CalculateColumnRow(i, gridConfig.ColumnCount, out column, out row);
                grid.GetCell(column, row).Content = visualContent.Instantiate();
            }

            return grid;
        }
    }
}