using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(10)]
public class PlayerStats
{
    public int totalGold;

    public int goldPerSecond;

    public PlayerStats(int _totalGold, int _goldPerClick, int _goldPerSecond)
    {
        totalGold = _totalGold;
        goldPerSecond = _goldPerSecond;
    }

    public PlayerStats()
    {
        totalGold = 0;
        goldPerSecond = 0;
    }
}

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo current;

    public PlayerStats playerStats;

    private LayerMask clickableLayerMask;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        clickableLayerMask = LayerMask.GetMask("Clickable");
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = new PlayerStats();
    }

    public void handlePlayerClick()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40, clickableLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        BoardManager boardManager = SceneManagerManager.current.getBoardManager();

        bool isCell = hit.collider.gameObject.CompareTag("Cell");
        if (!isCell)
        {
            return;
        }

        if (boardManager.buildPrep)
        {
            this.handleBuildPrepClick(hit.collider.gameObject);
            return;
        }

        this.handleClick(hit.collider.gameObject);
    }

    private void handleBuildPrepClick(GameObject cellGameObject)
    {
        BoardCell boardCell = cellGameObject.GetComponent(typeof(BoardCell)) as BoardCell;
        if (boardCell == null || boardCell.isAlive)
        {
            return;
        }

        BoardManager boardManager = SceneManagerManager.current.getBoardManager();
        SingleClassLevel level = boardManager.buildLevel;
        boardCell.initToLevel(level);
        boardManager.exitBuildPrep();
        playerStats.totalGold = playerStats.totalGold - level.goldRequirement;
    }

    private void handleClick(GameObject cellGameObject)
    {
        BoardCell boardCell = cellGameObject.GetComponent(typeof(BoardCell)) as BoardCell;
        if (boardCell == null || !boardCell.isAlive)
        {
            return;
        }
        this.handleTick();
    }

    public void handleTick()
    {
        if (!SceneManagerManager.current)
        {
            return;
        }

        playerStats.totalGold =
            playerStats.totalGold + SceneManagerManager.current.getBoardManager().goldPerClick;
    }
}
