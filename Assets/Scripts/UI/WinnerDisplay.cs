using UnityEngine;
using TMPro;

public class WinnerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp;

    private void Start()
    {
        tmp.text = "";
    }

    public void ShowPlayer1Win()
    {
        tmp.text = "Joueur 1 a gagné !";
    }

    public void ShowPlayer2Win()
    {
        tmp.text = "Joueur 2 a gagné !";
    }

    public void ShowDraw()
    {
        tmp.text = "Égalité !";
    }
}
