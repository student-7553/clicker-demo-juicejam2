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
