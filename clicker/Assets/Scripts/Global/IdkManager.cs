using UnityEngine;

public class MainGameLocalManagers
{
    public BoardManager boardManager;
}

[DefaultExecutionOrder(-10)]
public class IdkManager : MonoBehaviour
{
    public static IdkManager current;

    private MainGameLocalManagers mainGameLocalManagers;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        mainGameLocalManagers = new MainGameLocalManagers();
    }

    public BoardManager getBoardManager()
    {
        return mainGameLocalManagers.boardManager;
    }

    public void registerBoardManager(BoardManager manager)
    {
        mainGameLocalManagers.boardManager = manager;
    }

    public void clearBoardManager()
    {
        mainGameLocalManagers.boardManager = null;
    }
}
