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

    [SerializeField]
    private Levels levelHandler;

    private int maxLevels;
    private int _currentLevel;
    public int currentLevel
    {
        get { return _currentLevel; }
        set
        {
            _currentLevel = value;

            if (maxLevels != 0)
            {
                SceneManagerManager.current
                    .getBoardManager()
                    ?.onLevelIncrease(_currentLevel, maxLevels);
            }
        }
    }

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

    void Start()
    {
        this.currentLevel = 0;
        this.maxLevels = this.levelHandler.trueLevels.Count;

        playerStats = new PlayerStats();
    }

    public void handlePlayerClick()
    {
        BoardManager boardManager = SceneManagerManager.current.getBoardManager();
        if (boardManager == null)
        {
            return;
        }

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40, clickableLayerMask);

        if (hit.collider == null)
        {
            return;
        }

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
        if (level.level > this.currentLevel)
        {
            Debug.Log("level/" + level.level);
            this.currentLevel = level.level;
        }
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
        BoardManager boardManager = SceneManagerManager.current.getBoardManager();
        if (boardManager == null)
        {
            return;
        }

        playerStats.totalGold = playerStats.totalGold + boardManager.goldPerClick;
    }
}
