using System;

namespace DH.GridSystem.Cell
{
    public interface ICellContent
    {
        Action<Cell> OnCellChanged { get; set; }
    }

    public class NullContent : ICellContent
    {
        public Action<Cell> OnCellChanged { get; set; }
    }
}