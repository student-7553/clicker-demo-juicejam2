using UnityEngine;

public class RemoveButtonHandler : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    public void onClick()
    {
        boardManager.enterDestoryPhase();
    }
}
