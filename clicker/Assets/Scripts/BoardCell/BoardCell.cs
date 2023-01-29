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
    private BoardCellSprites boardCellSprites;

    public BoardCellType type;

    private BoardCellSpriteHandler boardCellSpriteHandler;

    private BoardCellBaseStats baseStats;

    // **************** MIDDLEWARE ******************

    public bool isAlive
    {
        get { return _isAlive; }
    }

    public BoardCellLevel boardCellLevel
    {
        get { return _boardCellLevel; }
        set
        {
            if (_boardCellLevel == value)
            {
                return;
            }
            _boardCellLevel = value;
            this.boardCellSpriteHandler.handleLevelChange(type, _boardCellLevel);
        }
    }

    private void Awake()
    {
        _isAlive = false;
        SpriteRenderer spriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        boardCellSpriteHandler = new BoardCellSpriteHandler(spriteRenderer, boardCellSprites);
    }

    public void init(BoardCellType _type, BoardCellBaseStats _baseStats)
    {
        type = _type;
        baseStats = _baseStats;
        boardCellLevel = this.getLevel(baseStats);
        _isAlive = true;
    }

    private BoardCellLevel getLevel(BoardCellBaseStats _baseStats)
    {
        // Todo
        return BoardCellLevel.LEVEL_1;
    }
}
