using UnityEngine;

public class BoardCellSpriteHandler
{
    public SpriteRenderer runeSpriteRenderer;
    public SpriteRenderer borderSpriteRenderer;
    private ClassBlock currentBlock;

    public float hoverBorderOpacity = 0.8f;

    public float defaultBorderOpacity = 0.5f;

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

    public void changeToBlock(ClassBlock _currentBlock)
    {
        if (_currentBlock == null)
        {
            currentBlock = null;
            runeSpriteRenderer.sprite = null;
            return;
        }

        currentBlock = _currentBlock;
        runeSpriteRenderer.sprite = _currentBlock.sprite;
        runeSpriteRenderer.color = _currentBlock.color;
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
                    currentBlock == null ? 1f : currentBlock.color.a
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
