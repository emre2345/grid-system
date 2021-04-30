namespace DH.Grid
{
    public class Cell
    {
        private int column;
        private int row;

        public int Column => column;

        public int Row => row;

        private ICellContent content;

        public ICellContent Content
        {
            get { return content; }
            set { content = value; }
        }

        public Cell(int column, int row)
        {
            this.column = column;
            this.row = row;
        }
    }
}