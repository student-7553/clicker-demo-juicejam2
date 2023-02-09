using UnityEngine;

public class BoardCell : MonoBehaviour
{
    // **************** VARIABLES ******************
    [SerializeField]
    private bool _isAlive;

    [SerializeField]
    private Levels levelsHandler;

    [SerializeField]
    private GameObject runeGameObject;

    private BoardCellSpriteHandler boardCellSpriteHandler;

    public BoardCellRuneStats runeStats;

    private PolygonCollider2D polygonCollider;

    private BoardManager parentBoardManager;

    private bool buildPrep = true;

    // **************** MIDDLEWARE ******************

    public bool isAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            if (polygonCollider != null)
            {
                polygonCollider.enabled = _isAlive;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (buildPrep || !isAlive)
        {
            return;
        }
        boardCellSpriteHandler.onHover();
    }

    private void OnMouseExit()
    {
        if (buildPrep || !isAlive)
        {
            return;
        }
        boardCellSpriteHandler.onHover(true);
    }

    public void enterBuildPrep()
    {
        if (!buildPrep)
        {
            return;
        }
        buildPrep = true;
        if (isAlive)
        {
            boardCellSpriteHandler.runeSpriteRenderer.color = new Color(
                boardCellSpriteHandler.runeSpriteRenderer.color.r,
                boardCellSpriteHandler.runeSpriteRenderer.color.g,
                boardCellSpriteHandler.runeSpriteRenderer.color.b,
                boardCellSpriteHandler.deadOpacity
            );
            boardCellSpriteHandler.borderSpriteRenderer.color = new Color(
                boardCellSpriteHandler.borderSpriteRenderer.color.r,
                boardCellSpriteHandler.borderSpriteRenderer.color.g,
                boardCellSpriteHandler.borderSpriteRenderer.color.b,
                boardCellSpriteHandler.deadOpacity
            );
            return;
        }
        boardCellSpriteHandler.borderSpriteRenderer.color = new Color(
            boardCellSpriteHandler.borderSpriteRenderer.color.r,
            boardCellSpriteHandler.borderSpriteRenderer.color.g,
            boardCellSpriteHandler.borderSpriteRenderer.color.b,
            boardCellSpriteHandler.hoverBorderOpacity
        );
    }

    public void exitBuildPrep()
    {
        if (buildPrep)
        {
            return;
        }
        buildPrep = false;
        if (isAlive)
        {
            boardCellSpriteHandler.runeSpriteRenderer.color = new Color(
                boardCellSpriteHandler.runeSpriteRenderer.color.r,
                boardCellSpriteHandler.runeSpriteRenderer.color.g,
                boardCellSpriteHandler.runeSpriteRenderer.color.b,
                1f
            );
            boardCellSpriteHandler.borderSpriteRenderer.color = new Color(
                boardCellSpriteHandler.borderSpriteRenderer.color.r,
                boardCellSpriteHandler.borderSpriteRenderer.color.g,
                boardCellSpriteHandler.borderSpriteRenderer.color.b,
                boardCellSpriteHandler.defaultOpacity
            );
            return;
        }
        boardCellSpriteHandler.borderSpriteRenderer.color = new Color(
            boardCellSpriteHandler.borderSpriteRenderer.color.r,
            boardCellSpriteHandler.borderSpriteRenderer.color.g,
            boardCellSpriteHandler.borderSpriteRenderer.color.b,
            boardCellSpriteHandler.deadOpacity
        );
    }

    private void Awake()
    {
        _isAlive = false;
        SpriteRenderer runeSpriteRenderer =
            runeGameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        SpriteRenderer borderSpriteRenderer =
            GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        polygonCollider = gameObject.GetComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;

        boardCellSpriteHandler = new BoardCellSpriteHandler(
            runeSpriteRenderer,
            borderSpriteRenderer,
            levelsHandler
        );
    }

    public void changeLevel(SingleLevel level)
    {
        isAlive = true;
        runeStats = level.runeStats;
        this.boardCellSpriteHandler.handleLevel(level);
        this.getBoardManager().resyncGoldPerClick();
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
