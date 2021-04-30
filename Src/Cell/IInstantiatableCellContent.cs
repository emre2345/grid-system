using UnityEngine;

namespace DH.GridSystem.Cell
{
    public interface IInstantiatableCellContent : ICellContent
    {
        IInstantiatableCellContent Instantiate();
    }
}