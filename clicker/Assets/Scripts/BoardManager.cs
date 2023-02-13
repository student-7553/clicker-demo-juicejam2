using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private List<List<BoardCell>> board;

    [SerializeField]
    private GameObject boardCellPrefab;

    [SerializeField]
    private Vector2 tempAnchorPoint;

    private float gridSpace = 0.677f;
    private float bottomOffsetSpace = 0.12f;

    [SerializeField]
    private Levels levelsHandler;

    private int size = 9;

    public int goldPerClick = 0;

    public bool buildPrep = false;
    public SingleClassLevel buildLevel;

    private void Start()
    {
        // Don't have save file
        board = this.generateBoard();

        // Initlize the first cell
        BoardCell centerCell = board[(int)size / 2][(int)size / 2];
        centerCell.initToLevel(levelsHandler.trueLevels[0]);
    }

    private List<List<BoardCell>> generateBoard()
    {
        List<List<BoardCell>> emptyBoard = new List<List<BoardCell>>();
        for (int columnIndex = 0; columnIndex < size; columnIndex++)
        {
            List<BoardCell> emptyRow = new List<BoardCell>();
            for (int rowIndex = 0; rowIndex < size; rowIndex++)
            {
                Vector3 cellSpawnPostion = getCellPosition(rowIndex, columnIndex);

                GameObject cellGameObject = Instantiate(
                    boardCellPrefab,
                    cellSpawnPostion,
                    Quaternion.identity,
                    this.transform
                );
                BoardCell cell = cellGameObject.GetComponent(typeof(BoardCell)) as BoardCell;
                emptyRow.Add(cell);
            }
            emptyBoard.Add(emptyRow);
        }
        return emptyBoard;

        Vector3 getCellPosition(int rowIndex, int columnIndex)
        {
            if (columnIndex % 2 == 0)
            {
                return new Vector3(
                    tempAnchorPoint.x + (gridSpace * rowIndex),
                    tempAnchorPoint.y
                        + (gridSpace * columnIndex)
                        - (bottomOffsetSpace * columnIndex),
                    0f
                );
            }
            else
            {
                return new Vector3(
                    tempAnchorPoint.x + (gridSpace * rowIndex) - (gridSpace / 2),
                    tempAnchorPoint.y
                        + (gridSpace * columnIndex)
                        - (bottomOffsetSpace * columnIndex),
                    0f
                );
            }
        }
    }

    public void resyncGoldPerClick()
    {
        int newGoldPerClick = 0;

        foreach (CellWithPosition cellPosition in getAllCells())
        {
            if (!cellPosition.cell.isAlive)
            {
                continue;
            }
            newGoldPerClick = newGoldPerClick + cellPosition.cell.runeStats.goldPerClickIncease;
        }
        goldPerClick = newGoldPerClick;
    }

    public void enterBuildPrep(SingleClassLevel prepLevel)
    {
        if (this.buildPrep == true)
        {
            return;
        }
        this.buildPrep = true;
        this.buildLevel = prepLevel;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.enterBuildPrep();
        }
    }

    public void exitBuildPrep()
    {
        if (this.buildPrep == false)
        {
            return;
        }
        this.buildPrep = false;
        this.buildLevel = null;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.exitBuildPrep();
        }
    }

    public IEnumerable<CellWithPosition> getAllCells()
    {
        for (int columnIndex = 0; columnIndex < board.Count; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < board[columnIndex].Count; rowIndex++)
            {
                yield return new CellWithPosition(
                    board[columnIndex][rowIndex],
                    columnIndex,
                    rowIndex
                );
            }
        }
    }

    private void Awake()
    {
        if (SceneManagerManager.current != null)
        {
            SceneManagerManager.current.mainGameLocalManagers.boardManager = this;
        }
    }

    private void OnDestroy()
    {
        if (SceneManagerManager.current != null)
        {
            SceneManagerManager.current.mainGameLocalManagers.boardManager = null;
        }
    }
}
