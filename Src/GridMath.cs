namespace DH.GridSystem
{
    public static class GridMath
    {
        public static void CalculateColumnRow(int index, int columnCount, out int column, out int row)
        {
            column = index % columnCount;
            row = (index - column) / columnCount;
        }

        public static int CalculateIndexFromColumnRow(int columnCount, int column, int row)
        {
            return column + columnCount * row;
        }
    }
}