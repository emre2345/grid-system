namespace DH.Grid
{
    public static class GridMath
    {
        public static void CalculateColumnRow(int index, int columnCount, int rowCount, out int column, out int row)
        {
            column = index % columnCount;
            row = (index - column) / columnCount;
        }
    }
}