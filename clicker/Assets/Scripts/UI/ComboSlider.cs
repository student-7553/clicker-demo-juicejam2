using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSlider : MonoBehaviour
{
    public ClickCombo clickCombo;
    private Slider selfSlider;

    void Start()
    {
        this.selfSlider = GetComponent(typeof(Slider)) as Slider;
        if (this.selfSlider == null)
        {
            throw new System.Exception("[MISSING COMPONENT] slider missing ");
        }
    }

    void FixedUpdate()
    {
        this.selfSlider.value = clickCombo.clickCombo;
    }
}
