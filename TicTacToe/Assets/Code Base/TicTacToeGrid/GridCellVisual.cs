using UnityEngine;
using UnityEngine.UI;

namespace Code_Base.TicTacToeGrid
{
  public class GridCellVisual : MonoBehaviour
  {
    private static readonly int HINT_STATE = Animator.StringToHash("hint");

    public Button GridCellButton;
    public Image GridCellImage;

    [SerializeField] private Animator cellAnimator;
    
    public void ClearGridCellVisual()
    {
      GridCellImage.color = new Color(255, 255, 255, 0);
      GridCellImage.sprite = null;
      SetButtonInteractable(true);
    }

    public void SetMark(Sprite sprite)
    {
      GridCellImage.color = new Color(255, 255, 255, 1);
      GridCellImage.sprite = sprite;
    }

    public void SetButtonInteractable(bool interactable) => 
      GridCellButton.interactable = interactable;

    public void Outline() => 
      cellAnimator.SetTrigger(HINT_STATE);
  }
}