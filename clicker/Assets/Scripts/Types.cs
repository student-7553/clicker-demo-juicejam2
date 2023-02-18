using UnityEngine;

[System.Serializable]
public struct BoardCellRuneStats
{
    // Increases the gold per click
    [Tooltip("Gold per Click")]
    public int goldPerClickIncease;

    // If adjacent to a goldPerClickIncease increases the gold per click
    [Tooltip("Battery power")]
    public int batteryPower;

    // Boosts all adjacent goldPerClickIncease and batteryPower
    [Tooltip("Booter power")]
    public int boosterPower;

    // Boosts all adjacent goldPerClickIncease, batteryPower,booster
    [Tooltip("Booter all power")]
    public int boosterAllPower;
}

public enum CellRuneType
{
    dead,
    goldPerClick,
    batteryPower,
    boosterPower,
    boosterAllPower
}

public struct CellWithPosition
{
    public BoardCell cell;
    public int positionY;
    public int positionX;

    public CellWithPosition(BoardCell _cell, int _positionY, int _positionX)
    {
        cell = _cell;
        positionY = _positionY;
        positionX = _positionX;
    }
}
