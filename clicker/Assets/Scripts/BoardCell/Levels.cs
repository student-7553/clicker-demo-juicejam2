using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SingleLevel
{
    public int level;
    public Sprite sprite;
    public string name;
    public int scoreRequirement;
    public string color;
    public Vector2Int runePosition;
    public BoardCellBaseStats runeStats;
};

[CreateAssetMenu(
    fileName = "LevelsObjects",
    menuName = "ScriptableObjects/LevelsObjects",
    order = 1
)]
public class Levels : ScriptableObject
{
    [SerializeField]
    public List<SingleLevel> levels;
}
