using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DH.GridSystem.Cell
{
    public struct CellCollection
    {
        private readonly List<Cell> cells;

        public int Count
        {
            get { return cells.Count; }
        }

        public Cell this[int i]
        {
            get { return cells[i]; }
        }

        public CellCollection([NotNull] IEnumerable<Cell> collection)
        {
            cells = new List<Cell>(collection);
        }

        public CellCollection(CellCollection collection)
        {
            this.cells = new List<Cell>(collection.cells);
        }

        public void Foreach(Action<Cell> dlg)
        {
            cells.ForEach(dlg);
        }

        public List<Cell> CellsWithContent
        {
            get { return cells.FindAll(c => c.Content.GetType() != typeof(NullContent)); }
        }
    }
}