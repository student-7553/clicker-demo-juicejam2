using UnityEngine;

[CreateAssetMenu(fileName = "Variables", menuName = "ScriptableObjects/Variables", order = 1)]
public class Variables : ScriptableObject
{
    public Vector2 tempAnchorPoint = new Vector2(-4.5f, -1.75f);
    public float gridSpace = 0.677f;
    public float bottomOffsetSpace = 0.12f;
    public int size = 9;
    public int blockIncrease = 10;
    public int maxiumPowerPerClick = 500;
}
