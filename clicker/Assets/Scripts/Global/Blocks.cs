using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct StructBlock
{
    public int level;
    public Sprite sprite;
    public string name;
    public int goldRequirement;
    public Color color;
    public int colorPower;
    public BoardCellRuneStats runeStats;
    public int charge;
};

public class ClassBlock
{
    public int level;
    public Sprite sprite;
    public string name;
    public int goldRequirement;

    // public Color color;
    public int colorPower;
    public BoardCellRuneStats runeStats;
    public int charge;
};

[CreateAssetMenu(
    fileName = "BlocksObject",
    menuName = "ScriptableObjects/BlocksObjects",
    order = 1
)]
public class Blocks : ScriptableObject
{
    [SerializeField]
    private List<StructBlock> structBlocks;

    public List<ClassBlock> blocks;

    void OnEnable()
    {
        // This should probably be a seperate class
        blocks = new List<ClassBlock>();
        foreach (StructBlock block in structBlocks)
        {
            ClassBlock classBlock = new ClassBlock();
            classBlock.level = block.level;
            classBlock.sprite = block.sprite;
            classBlock.name = block.name;
            // classBlock.color = block.color;
            classBlock.colorPower = block.colorPower;
            classBlock.runeStats = block.runeStats;
            classBlock.charge = block.charge;
            classBlock.goldRequirement = block.goldRequirement;
            blocks.Add(classBlock);
        }
    }

    public ClassBlock getBlock(int level)
    {
        foreach (ClassBlock block in blocks)
        {
            if (block.level == level)
            {
                return block;
            }
        }
        return null;
    }
}
