using UnityEngine;

public class ClickCombo : MonoBehaviour
{
    [SerializeField]
    private Variables variables;

    [SerializeField]
    private BoardManager boardManager;

    private float tickTimer;

    // Range 0-1
    private float _clickCombo;
    public float clickCombo
    {
        get { return _clickCombo; }
        set
        {
            if (value < 0f)
            {
                value = 0f;
            }
            if (value > 1f)
            {
                value = 1f;
            }
            if (this.isOnFire() && !this.isOnFire(value))
            {
                this.boardManager.changeBorderColor();
            }
            if (!this.isOnFire() && this.isOnFire(value))
            {
                this.boardManager.changeBorderColor();
            }
            _clickCombo = value;
        }
    }

    void Start()
    {
        clickCombo = 0;
        tickTimer = 0;
    }

    private void FixedUpdate()
    {
        this.handleTick();
    }

    public bool isOnFire()
    {
        return this.clickCombo > variables.comboDoubleThreshhold;
    }

    public bool isOnFire(float checkClickCombo)
    {
        return checkClickCombo > variables.comboDoubleThreshhold;
    }

    public void handlePlayerTick()
    {
        clickCombo = clickCombo + variables.comboPerTick;
    }

    private void handleTick()
    {
        tickTimer = tickTimer + Time.fixedDeltaTime;
        if (tickTimer > 1f)
        {
            tickTimer = 0f;
            clickCombo = clickCombo - variables.comboDecreasePerSec;
        }
    }
}
