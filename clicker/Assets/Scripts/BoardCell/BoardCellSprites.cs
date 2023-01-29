using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct LevelSprite
{
    public int level;
    public Sprite sprite;
};

[CreateAssetMenu(
    fileName = "BoardCellSprites",
    menuName = "ScriptableObjects/BoardCellSprites",
    order = 1
)]
public class BoardCellSprites : ScriptableObject
{
    // public void OnEnable()
    // {
    //     Debug.Log("OnEnable");
    // }

    [SerializeField]
    public List<LevelSprite> goldPerClickSprites;
    public List<LevelSprite> goldPerSecondSprites;
    public List<LevelSprite> boostAllSprites;
    public List<LevelSprite> boostSpecificSprites;
}
