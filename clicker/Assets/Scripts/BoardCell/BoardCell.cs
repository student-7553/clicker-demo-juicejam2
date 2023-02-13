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

    // **************** MIDDLEWARE ******************

    public bool isAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    private void OnMouseEnter()
    {
        if (!isAlive && getBoardManager().buildPrep)
        {
            boardCellSpriteHandler.onHover();
            return;
        }

        if (!isAlive || getBoardManager().buildPrep)
        {
            return;
        }

        boardCellSpriteHandler.onHover();
    }

    private void OnMouseExit()
    {
        if (!isAlive && getBoardManager().buildPrep)
        {
            boardCellSpriteHandler.onHover(true);
            return;
        }

        if (!isAlive || getBoardManager().buildPrep)
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

    public void initToLevel(SingleClassLevel level)
    {
        isAlive = true;
        runeStats = level.runeStats;

        this.boardCellSpriteHandler.changeLevel(level);
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
