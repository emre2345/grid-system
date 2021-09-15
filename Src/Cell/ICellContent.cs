using System;

namespace DH.GridSystem.Cell
{
    public interface ICellContent
    {
        Action<Cell> OnCellChanged { get; set; }
    }

    public class NullContent : ICellContent
    {
        private static Lazy<NullContent> instance = new Lazy<NullContent>(() => new NullContent());
        public static NullContent Instance => instance.Value;
        public Action<Cell> OnCellChanged { get; set; }
    }
}