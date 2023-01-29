using UnityEngine;

public class BoardCellSpriteHandler
{
    private SpriteRenderer spriteRenderer;
    private BoardCellSprites boardCellSprites;
    private BoardCellType boardCellType;

    public BoardCellSpriteHandler(
        SpriteRenderer _spriteRenderer,
        BoardCellSprites _boardCellSprites
    )
    {
        spriteRenderer = _spriteRenderer;
        boardCellSprites = _boardCellSprites;
    }

    public void handleLevelChange(BoardCellType _type, BoardCellLevel _boardCellLevel) { }
}
