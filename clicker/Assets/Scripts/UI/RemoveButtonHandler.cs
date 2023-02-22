using UnityEngine;
using UnityEngine.UI;

public class RemoveButtonHandler : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    private Button selfButton;

    private void Awake()
    {
        selfButton = GetComponent(typeof(Button)) as Button;
    }

    public void onClick()
    {
        if (boardManager.phase == BoardPhases.normal)
        {
            boardManager.enterDestoryPhase();
            return;
        }

        boardManager.exitPhases();
    }
}
