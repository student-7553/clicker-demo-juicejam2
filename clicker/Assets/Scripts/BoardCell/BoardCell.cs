using UnityEngine;

public class BoardCell : MonoBehaviour
{
    // **************** VARIABLES ******************
    [SerializeField]
    private bool _isAlive;

    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private GameObject runeGameObject;

    private BoardCellSpriteHandler boardCellSpriteHandler;

    public BoardCellRuneStats runeStats;

    public CellRuneType runeType;

    public bool isBooster = false;

    public int powerValue;

    private PolygonCollider2D polygonCollider;

    private BoardManager parentBoardManager;

    // **************** PROXY ******************

    public bool isAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    private void OnMouseEnter()
    {
        if (!isAlive && getBoardManager().inBuildPrep)
        {
            boardCellSpriteHandler.onHover();
            return;
        }

        if (!isAlive || getBoardManager().inBuildPrep)
        {
            return;
        }

        boardCellSpriteHandler.onHover();
    }

    private void OnMouseExit()
    {
        if (!isAlive && getBoardManager().inBuildPrep)
        {
            boardCellSpriteHandler.onHover(true);
            return;
        }

        if (!isAlive || getBoardManager().inBuildPrep)
        {
            return;
        }
        boardCellSpriteHandler.onHover(true);
    }

    public void enterBuildPrep()
    {
        boardCellSpriteHandler.handlePrep(isAlive);
    }

    public void exitBuildPrep()
    {
        boardCellSpriteHandler.handlePrep(isAlive, true);
    }

    public void changeBorderColor(float percentage)
    {
        float newR =
            blocksHandler.startBorderColor.r
            + ((blocksHandler.endBorderColor.r - blocksHandler.startBorderColor.r) * percentage);
        float newG =
            blocksHandler.startBorderColor.g
            + ((blocksHandler.endBorderColor.g - blocksHandler.startBorderColor.g) * percentage);
        float newB =
            blocksHandler.startBorderColor.b
            + ((blocksHandler.endBorderColor.b - blocksHandler.startBorderColor.b) * percentage);
        float newA =
            blocksHandler.startBorderColor.a
            + ((blocksHandler.endBorderColor.a - blocksHandler.startBorderColor.a) * percentage);

        Color newBorderColor = new Color(newR, newG, newB, newA);
        boardCellSpriteHandler.changeBorderColor(newBorderColor);
    }

    private void Awake()
    {
        _isAlive = false;
        SpriteRenderer runeSpriteRenderer =
            runeGameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        SpriteRenderer borderSpriteRenderer =
            GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        polygonCollider = gameObject.GetComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;

        polygonCollider.enabled = true;

        boardCellSpriteHandler = new BoardCellSpriteHandler(
            runeSpriteRenderer,
            borderSpriteRenderer
        );
    }

    public void initToLevel(ClassBlock block)
    {
        isAlive = true;

        runeStats = block.runeStats;

        if (runeStats.goldPerClickIncease > 0)
        {
            runeType = CellRuneType.goldPerClick;
            powerValue = runeStats.goldPerClickIncease;
        }
        else if (runeStats.batteryPower > 0)
        {
            runeType = CellRuneType.batteryPower;
            powerValue = runeStats.batteryPower;
        }
        else if (runeStats.boosterPower > 0)
        {
            runeType = CellRuneType.boosterPower;
            powerValue = runeStats.boosterPower;
            isBooster = true;
        }
        else if (runeStats.boosterAllPower > 0)
        {
            runeType = CellRuneType.boosterAllPower;
            powerValue = runeStats.boosterAllPower;
            isBooster = true;
        }

        this.boardCellSpriteHandler.initBLock(block);
    }

    private BoardManager getBoardManager()
    {
        if (parentBoardManager != null)
        {
            return parentBoardManager;
        }

        parentBoardManager =
            this.transform.parent.gameObject.GetComponent(typeof(BoardManager)) as BoardManager;

        return parentBoardManager;
    }
}
