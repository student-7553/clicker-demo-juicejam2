using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    // **************** VARIABLES ******************
    [SerializeField]
    private bool _isAlive;

    public BoardCellLevel _boardCellLevel;

    [SerializeField]
    private Levels levelsHandler;

    private BoardCellSpriteHandler boardCellSpriteHandler;

    private BoardCellBaseStats baseStats;

    // **************** MIDDLEWARE ******************

    public bool isAlive
    {
        get { return _isAlive; }
    }

    public void OnMouseEnter() { }

    public void OnMouseExit() { }

    private void Awake()
    {
        _isAlive = false;
        SpriteRenderer spriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        boardCellSpriteHandler = new BoardCellSpriteHandler(spriteRenderer, levelsHandler);
    }

    public void init(SingleLevel level)
    {
        // Make it alive
        baseStats = level.runeStats;
        _isAlive = true;
        this.boardCellSpriteHandler.handleLevel(level);
    }
}
