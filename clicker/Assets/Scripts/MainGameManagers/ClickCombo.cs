using UnityEngine;

public class ClickCombo : MonoBehaviour
{
    public Variables variables;

    private float tickTimer;

    // [System.NonSerialized]
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
