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
        // selfButton.navigation
        if (boardManager.phase == BoardPhases.normal)
        {
            boardManager.enterDestoryPhase();
        }
        else if (boardManager.phase == BoardPhases.destroy)
        {
            boardManager.exitPhases();
        }
    }
}
