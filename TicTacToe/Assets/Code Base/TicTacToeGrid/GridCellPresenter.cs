using System;
using Code_Base.Enums;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.NextPlayerService;
using UnityEngine;

namespace Code_Base.TicTacToeGrid
{
  public class GridCellPresenter
  {
    public event Action<GridCellPresenter> OnGridCellOccupied;
    public event Action<GridCellPresenter> OnGridCellCleared;
    
    private readonly GridCell gridCell;
    private readonly GridCellVisual gridCellVisual;
    
    private readonly IPlayersOrderService playersOrderService;
    private readonly IGridSignFactory gridSignFactory;
    private readonly ICommandService commandService;
    private readonly IAssetBundleLoadService assetBundleLoadService;
    private readonly IAssetsFromBundleProviderService assetsFromBundleProviderService;
    
    
    
    public GridCellPresenter(
      GridCell gridCell,
      GridCellVisual gridCellVisual,
      IPlayersOrderService playersOrderService,
      IGridSignFactory gridSignFactory, 
      ICommandService commandService
      )
    {
      this.gridCell = gridCell;
      this.gridCellVisual = gridCellVisual;
      this.playersOrderService = playersOrderService;
      this.gridSignFactory = gridSignFactory;
      this.commandService = commandService;
    }
    
    public void Initialize() => 
      gridCellVisual.GridCellButton.onClick.AddListener(MakeMoveCommand);

    public void MakeMoveCommand() => 
      commandService.MakeMoveCommand(this);

    public void ClearGridCell()
    {
      gridCell.ClearGridCell();
      gridCellVisual.ClearGridCellVisual();
      OnGridCellCleared?.Invoke(this);
    }

    public void SetGridCellEnabled(bool enabled) => 
      gridCellVisual.GridCellButton.interactable = enabled;

    public void OccupyGridCell()
    {
      if (gridCell.OccupiedBy == PlayerType.NoPlayer)
      {
        gridCell.OccupyBy(playersOrderService.CurrentPlayer.PlayerType);
        UpdateGridCellMark();
        OnGridCellOccupied?.Invoke(this);
      }
    }

    public void ShowHintOutline() => 
      gridCellVisual.Outline();

    private void UpdateGridCellMark()
    {
      Sprite sign;
      
      switch (playersOrderService.CurrentPlayer.SignType)
      {
        case SignType.Cross:
        {
          sign = gridSignFactory.CreateXSign();
          break;
        }
        case SignType.Circle:
        {
          sign = gridSignFactory.CreateOSign();
          break;
        }
        default:
          throw new ArgumentOutOfRangeException(nameof(playersOrderService.CurrentPlayer.SignType), "Unknown sign");
      }
      
      UpdateGridCellVisual(sign);
    }

    private void UpdateGridCellVisual(Sprite sign)
    {
      gridCellVisual.SetMark(sign);
      gridCellVisual.SetButtonInteractable(false);
    }
  }
}