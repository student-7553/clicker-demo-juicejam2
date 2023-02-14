using UnityEngine;

public class BoardCellSpriteHandler
{
    public SpriteRenderer runeSpriteRenderer;
    public SpriteRenderer borderSpriteRenderer;
    private SingleClassLevel currentLevel;

    public float hoverBorderOpacity = 0.65f;
    public float defaultBorderOpacity = 0.35f;
    public float deadOpacity = 0.15f;

    private float prevBorderHoverOpacity;

    public BoardCellSpriteHandler(
        SpriteRenderer _runeSpriteRenderer,
        SpriteRenderer _borderSpriteRenderer
    )
    {
        borderSpriteRenderer = _borderSpriteRenderer;
        runeSpriteRenderer = _runeSpriteRenderer;
    }

    public void onHover(bool isExiting = false)
    {
        if (!isExiting)
        {
            prevBorderHoverOpacity = borderSpriteRenderer.color.a;
        }

        borderSpriteRenderer.color = new Color(
            borderSpriteRenderer.color.r,
            borderSpriteRenderer.color.g,
            borderSpriteRenderer.color.b,
            isExiting ? prevBorderHoverOpacity : hoverBorderOpacity
        );
    }

    public void changeLevel(SingleClassLevel level)
    {
        currentLevel = level;
        runeSpriteRenderer.sprite = level.sprite;
        runeSpriteRenderer.color = level.color;
        borderSpriteRenderer.color = new Color(
            borderSpriteRenderer.color.r,
            borderSpriteRenderer.color.g,
            borderSpriteRenderer.color.b,
            defaultBorderOpacity
        );
    }

    public void handlePrep(bool isAlive, bool isExiting = false)
    {
        if (isExiting)
        {
            if (isAlive)
            {
                runeSpriteRenderer.color = new Color(
                    runeSpriteRenderer.color.r,
                    runeSpriteRenderer.color.g,
                    runeSpriteRenderer.color.b,
                    currentLevel == null ? 1f : currentLevel.color.a
                );
                borderSpriteRenderer.color = new Color(
                    borderSpriteRenderer.color.r,
                    borderSpriteRenderer.color.g,
                    borderSpriteRenderer.color.b,
                    defaultBorderOpacity
                );
                return;
            }
            borderSpriteRenderer.color = new Color(
                borderSpriteRenderer.color.r,
                borderSpriteRenderer.color.g,
                borderSpriteRenderer.color.b,
                deadOpacity
            );
        }
        else
        {
            if (isAlive)
            {
                runeSpriteRenderer.color = new Color(
                    runeSpriteRenderer.color.r,
                    runeSpriteRenderer.color.g,
                    runeSpriteRenderer.color.b,
                    deadOpacity
                );
                borderSpriteRenderer.color = new Color(
                    borderSpriteRenderer.color.r,
                    borderSpriteRenderer.color.g,
                    borderSpriteRenderer.color.b,
                    deadOpacity
                );
                return;
            }
            else
            {
                borderSpriteRenderer.color = new Color(
                    borderSpriteRenderer.color.r,
                    borderSpriteRenderer.color.g,
                    borderSpriteRenderer.color.b,
                    defaultBorderOpacity
                );
            }
        }
    }

    public void changeBorderColor(Color newBorderColor)
    {
        this.borderSpriteRenderer.color = new Color(
            newBorderColor.r,
            newBorderColor.g,
            newBorderColor.b,
            defaultBorderOpacity
        );
    }
}
