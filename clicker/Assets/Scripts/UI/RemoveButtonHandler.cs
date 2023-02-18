using UnityEngine;

public class RemoveButtonHandler : MonoBehaviour
{
    public void onClick()
    {
        IdkManager.current.getBoardManager()?.enterDestoryPhase();
    }
}
