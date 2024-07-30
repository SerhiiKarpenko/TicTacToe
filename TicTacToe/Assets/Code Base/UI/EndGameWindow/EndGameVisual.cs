using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code_Base.UI.EndGameWindow
{
  public class EndGameVisual : MonoBehaviour
  {
    public GameObject DrawGameObject;
    public GameObject WinGameObject;
    public TextMeshProUGUI WinnerText;
    public Button MainMenuButton;
    public Button RestartButton;

    public void SetWinnerText(string text) => 
      WinnerText.text = text;
  }
}