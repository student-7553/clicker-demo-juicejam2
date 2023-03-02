using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Variables", menuName = "ScriptableObjects/Variables", order = 1)]
public class Variables : ScriptableObject
{
    public Vector2 tempAnchorPoint = new Vector2(-5f, -2.125f);

    public float gridSpace = 0.8f;
    public float bottomOffsetSpace = 0.12f;

    public int size = 9;
    public int blockIncrease = 5;
    public int maxiumPowerPerClick = 250;
    public float comboPerTick = 0.02f;
    public float comboDecreasePerSec = 0.04f;

    public float comboDoubleThreshhold = 0.85f;

    public float clickEffectWait = 0.1f;
    public Vector3 clickEffectScale = new Vector3(0.95f, 0.95f, 1f);

    public List<Color> borderColors;

    public float minDrag = 10f;
    public float maxDrag = 0.8f;

    public float minSpeed = 2.5f;
    public float maxSpeed = 6f;

    public int maxFoldPerClickDifference = 10;
}
