using UnityEngine;

public class BoardCellSpriteHandler
{
    public SpriteRenderer runeSpriteRenderer;
    public SpriteRenderer borderSpriteRenderer;
    public Levels levelsHandler;

    public float hoverBorderOpacity = 0.5f;
    public float defaultOpacity = 0.3f;
    public float deadOpacity = 0.15f;

    public BoardCellSpriteHandler(
        SpriteRenderer _runeSpriteRenderer,
        SpriteRenderer _borderSpriteRenderer,
        Levels _levelsHandler
    )
    {
        borderSpriteRenderer = _borderSpriteRenderer;
        runeSpriteRenderer = _runeSpriteRenderer;
        levelsHandler = _levelsHandler;
    }

    public void onHover(bool isExiting = false)
    {
        borderSpriteRenderer.color = new Color(
            borderSpriteRenderer.color.r,
            borderSpriteRenderer.color.g,
            borderSpriteRenderer.color.b,
            isExiting ? defaultOpacity : hoverBorderOpacity
        );
    }

    public void handleLevel(SingleLevel level)
    {
        runeSpriteRenderer.sprite = level.sprite;
        runeSpriteRenderer.color = level.color;
    }
}
