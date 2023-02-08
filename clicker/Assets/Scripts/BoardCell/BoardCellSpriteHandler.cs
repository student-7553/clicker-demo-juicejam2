using UnityEngine;

public class BoardCellSpriteHandler
{
    private SpriteRenderer spriteRenderer;
    private Levels levelsHandler;

    public BoardCellSpriteHandler(SpriteRenderer _spriteRenderer, Levels _levelsHandler)
    {
        spriteRenderer = _spriteRenderer;
        levelsHandler = _levelsHandler;
    }

    public void handleLevel(SingleLevel level)
    {
        // change sprite with the one in level
        // get the color from level and change it here
    }
}
