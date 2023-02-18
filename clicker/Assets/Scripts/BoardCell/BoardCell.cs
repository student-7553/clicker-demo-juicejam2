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

    public CellRuneType runeType;

    public int powerValue;

    private PolygonCollider2D polygonCollider;

    private BoardManager parentBoardManager;

    public ClassBlock currentBlock;

    // **************** PROXY ******************

    public bool isAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    private void OnMouseEnter()
    {
        if (!isAlive && getBoardManager().phase == BoardPhases.build)
        {
            boardCellSpriteHandler.onHover();
            return;
        }

        if (!isAlive || getBoardManager().phase == BoardPhases.build)
        {
            return;
        }

        boardCellSpriteHandler.onHover();
    }

    private void OnMouseExit()
    {
        if (!isAlive && getBoardManager().phase == BoardPhases.build)
        {
            boardCellSpriteHandler.onHover(true);
            return;
        }

        if (!isAlive || getBoardManager().phase == BoardPhases.build)
        {
            return;
        }
        boardCellSpriteHandler.onHover(true);
    }

    public void changePhase(BoardPhases phase)
    {
        switch (phase)
        {
            case BoardPhases.build:
            case BoardPhases.destroy:
                boardCellSpriteHandler.handlePrep(isAlive);
                break;
            case BoardPhases.normal:
                boardCellSpriteHandler.handlePrep(isAlive, true);
                break;
            default:
                break;
        }
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
        runeType = CellRuneType.dead;
        currentBlock = null;

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

    public void initToBlock(ClassBlock block)
    {
        isAlive = true;
        currentBlock = block;
        if (block.runeStats.goldPerClickIncease > 0)
        {
            runeType = CellRuneType.goldPerClick;
            powerValue = block.runeStats.goldPerClickIncease;
        }
        else if (block.runeStats.batteryPower > 0)
        {
            runeType = CellRuneType.batteryPower;
            powerValue = block.runeStats.batteryPower;
        }
        else if (block.runeStats.boosterPower > 0)
        {
            runeType = CellRuneType.boosterPower;
            powerValue = block.runeStats.boosterPower;
        }
        else if (block.runeStats.boosterAllPower > 0)
        {
            runeType = CellRuneType.boosterAllPower;
            powerValue = block.runeStats.boosterAllPower;
        }

        this.boardCellSpriteHandler.changeToBlock(block);
    }

    public void destroyCurrentBlock()
    {
        if (!isAlive)
        {
            return;
        }
        isAlive = false;
        runeType = CellRuneType.dead;
        powerValue = 0;
        currentBlock = null;
        this.boardCellSpriteHandler.changeToBlock(null);
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
