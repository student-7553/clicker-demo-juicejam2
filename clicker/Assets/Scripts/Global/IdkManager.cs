using UnityEngine;

[DefaultExecutionOrder(-10)]
public class IdkManager : MonoBehaviour
{
    public static IdkManager current;

    private BoardManager boardManager;

    private BoardPlayerManager boardPlayerManager;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
    }

    public BoardManager getBoardManager()
    {
        return boardManager;
    }

    public void registerBoardManager(BoardManager _boardManager)
    {
        boardManager = _boardManager;
    }

    public void clearBoardManager()
    {
        boardManager = null;
    }

    public BoardPlayerManager getBoardPlayerManager()
    {
        return boardPlayerManager;
    }

    public void registerBoardPlayerManager(BoardPlayerManager _boardPlayerManager)
    {
        boardPlayerManager = _boardPlayerManager;
    }

    public void clearBoardPlayerManager()
    {
        boardPlayerManager = null;
    }
}
