using System;

namespace DH.GridSystem.Cell
{
    public class Cell
    {
        private int column;
        private int row;

        public int Column => column;

        public int Row => row;

        public Action<ICellContent> OnContentChanged
        {
            get;
            set;
        }

        private ICellContent content = new NullContent();

        public ICellContent Content
        {
            get { return content; }
            set
            {
                content = value;
                content.OnCellChanged?.Invoke(this);
                OnContentChanged?.Invoke(content);
            }
        }

        public Cell(int column, int row)
        {
            this.column = column;
            this.row = row;
        }
    }
}