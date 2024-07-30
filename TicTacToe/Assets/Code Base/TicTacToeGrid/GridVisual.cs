using Code_Base.Infrastructure.Services.UIFactory;
using UnityEngine;
using Zenject;

namespace Code_Base.TicTacToeGrid
{
  public class GridVisual : MonoBehaviour, IGridVisual
  {
    public GridCellVisual[,] GridCellVisuals { get; private set; }
    
    private IUIFactory uiFactory;
    
    [Inject]
    public void Construct(IUIFactory uiFactory)
    {
      this.uiFactory = uiFactory;
    }

    public void InitializeGridVisuals(int gridWidth, int gridHeight)
    {
      GridCellVisuals = new GridCellVisual[gridWidth, gridHeight];
      
      for (int column = 0; column < gridWidth; column++)
      {
        for (int row = 0; row < gridHeight; row++)
        {
          GridCellVisual gridCellVisual = uiFactory.CreateGirdCellVisual();
          GridCellVisuals[column, row] = gridCellVisual;
        }
      }
    }
  }
}