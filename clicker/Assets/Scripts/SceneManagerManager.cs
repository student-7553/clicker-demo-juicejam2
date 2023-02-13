using UnityEngine;

public class MainGameLocalManagers
{
    public BoardManager boardManager;
}

[DefaultExecutionOrder(-10)]
public class SceneManagerManager : MonoBehaviour
{
    public static SceneManagerManager current;

    public MainGameLocalManagers mainGameLocalManagers;

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
}
