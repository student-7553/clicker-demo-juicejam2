// public enum BoardCellType
// {
//     GOLD_PER_CLICK,
//     GOLD_PER_SEC,
//     BOOST_ALL,
//     BOOST_SPECIFIC
// }

// public enum BoardCellLevel
// {
//     LEVEL_1,
//     LEVEL_2,
//     LEVEL_3,
//     LEVEL_4
// }

[System.Serializable]
public struct BoardCellRuneStats
{
    public int goldPerClickIncease;
    public int goldPeSecIncease;
    public int boostAdjacentAll;
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
