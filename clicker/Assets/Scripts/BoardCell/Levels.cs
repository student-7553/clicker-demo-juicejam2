using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SingleLevel
{
    public int level;
    public Sprite sprite;
    public string name;
    public int goldRequirement;
    public Color color;
    public BoardCellRuneStats runeStats;
};

public class SingleClassLevel
{
    public int level;
    public Sprite sprite;
    public string name;
    public int goldRequirement;
    public Color color;
    public BoardCellRuneStats runeStats;
};

[CreateAssetMenu(
    fileName = "LevelsObjects",
    menuName = "ScriptableObjects/LevelsObjects",
    order = 1
)]
public class Levels : ScriptableObject
{
    [SerializeField]
    private List<SingleLevel> levels;

    public List<SingleClassLevel> trueLevels;

    public Color startBorderColor;

    public Color endBorderColor;

    void OnEnable()
    {
        trueLevels = new List<SingleClassLevel>();
        foreach (SingleLevel level in levels)
        {
            SingleClassLevel classLevel = new SingleClassLevel();
            classLevel.level = level.level;
            classLevel.sprite = level.sprite;
            classLevel.name = level.name;
            classLevel.goldRequirement = level.goldRequirement;
            classLevel.color = level.color;
            classLevel.runeStats = level.runeStats;
            trueLevels.Add(classLevel);
        }
    }
}
