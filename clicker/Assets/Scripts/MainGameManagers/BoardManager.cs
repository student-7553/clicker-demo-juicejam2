using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // ---------------- MAIN VAR ----------------------
    private List<List<BoardCell>> board;

    [System.NonSerialized]
    public int goldPerClick = 0;

    private Color boardBorderColor;

    [SerializeField]
    private int _blockCount;
    public int blockCount
    {
        get { return _blockCount; }
        set
        {
            _blockCount = value;
            this.handleBlockCountBorder(_blockCount);

            // Connect this with the actuall price on
        }
    }

    // ---------------- Build prep ----------------------

    [System.NonSerialized]
    public bool buildPrep = false;
    public int buildLevel;

    // ---------------- OUTSIDE  ----------------------
    [SerializeField]
    private GameObject boardCellPrefab;

    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private Variables variables;

    private void Awake()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.registerBoardManager(this);
        }
    }

    private void Start()
    {
        // Don't have save file
        board = this.generateBoard();

        // Initlize the first cell
        BoardCell centerCell = board[(int)variables.size / 2][(int)variables.size / 2];
        centerCell.initToLevel(blocksHandler.blocks[0]);

        boardBorderColor = blocksHandler.startBorderColor;
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
            int value = getBlocksValue(cellPosition);
            newGoldPerClick = newGoldPerClick + cellPosition.cell.runeStats.goldPerClickIncease;
        }

        goldPerClick = newGoldPerClick;

        int getBlocksValue(CellWithPosition cellPosition)
        {
            int value = 0;

            value = value + cellPosition.cell.runeStats.goldPerClickIncease;

            List<BoardCell> adjacentCells = this.getAllAdjacentCell(cellPosition);
            // adjacentCells
            return value;
        }
    }

    public void enterBuildPrep(ClassBlock prepLevel)
    {
        if (this.buildPrep == true)
        {
            return;
        }
        this.buildPrep = true;
        this.buildLevel = prepLevel.level;
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
        this.buildLevel = 0;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.exitBuildPrep();
        }
    }

    private void handleBlockCountBorder(float newBlockCount)
    {
        float maxLevels = blocksHandler.blocks.Count;
        float percentage = newBlockCount / maxLevels;

        float differnce =
            (blocksHandler.startBorderColor.r - blocksHandler.endBorderColor.r) / percentage;

        float newR =
            blocksHandler.startBorderColor.r
            + ((blocksHandler.endBorderColor.r - blocksHandler.startBorderColor.r) * percentage);
        float newG =
            blocksHandler.startBorderColor.g
            + ((blocksHandler.endBorderColor.g - blocksHandler.startBorderColor.g) * percentage);
        float newB =
            blocksHandler.startBorderColor.b
            + ((blocksHandler.endBorderColor.b - blocksHandler.startBorderColor.b) * percentage);
        float newA =
            blocksHandler.startBorderColor.a
            + ((blocksHandler.endBorderColor.a - blocksHandler.startBorderColor.a) * percentage);

        Color newBorderColor = new Color(newR, newG, newB, newA);
        boardBorderColor = newBorderColor;

        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.changeBorderColor(boardBorderColor);
        }
    }

    private void OnDestroy()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.clearBoardManager();
        }
    }

    private IEnumerable<CellWithPosition> getAllCells()
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

    private List<List<BoardCell>> generateBoard()
    {
        List<List<BoardCell>> emptyBoard = new List<List<BoardCell>>();
        for (int columnIndex = 0; columnIndex < variables.size; columnIndex++)
        {
            List<BoardCell> emptyRow = new List<BoardCell>();
            for (int rowIndex = 0; rowIndex < variables.size; rowIndex++)
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
                    variables.tempAnchorPoint.x + (variables.gridSpace * rowIndex),
                    variables.tempAnchorPoint.y
                        + (variables.gridSpace * columnIndex)
                        - (variables.bottomOffsetSpace * columnIndex),
                    0f
                );
            }
            else
            {
                return new Vector3(
                    variables.tempAnchorPoint.x
                        + (variables.gridSpace * rowIndex)
                        - (variables.gridSpace / 2),
                    variables.tempAnchorPoint.y
                        + (variables.gridSpace * columnIndex)
                        - (variables.bottomOffsetSpace * columnIndex),
                    0f
                );
            }
        }
    }

    private List<BoardCell> getAllAdjacentCell(CellWithPosition cellPosition)
    {
        List<BoardCell> cells = new List<BoardCell>();
        if (cellPosition.positionY != 0)
        {
            // Add the bottom row
            if (cellPosition.positionY % 2 == 0)
            {
                //is even
                // get right and same
                cells.Add(board[cellPosition.positionX][cellPosition.positionY - 1]);
                cells.Add(board[cellPosition.positionX + 1][cellPosition.positionY - 1]);
            }
            else
            {
                //is odd
                // get left and same
                cells.Add(board[cellPosition.positionX][cellPosition.positionY - 1]);
                cells.Add(board[cellPosition.positionX - 1][cellPosition.positionY - 1]);
            }
        }
        if (cellPosition.positionY != board.Count - 1)
        {
            // Add the top row

            if (cellPosition.positionY % 2 == 0)
            {
                //is even
                // get right and same
                cells.Add(board[cellPosition.positionX][cellPosition.positionY + 1]);
                cells.Add(board[cellPosition.positionX + 1][cellPosition.positionY + 1]);
            }
            else
            {
                //is odd
                // get left and same

                cells.Add(board[cellPosition.positionX][cellPosition.positionY + 1]);
                cells.Add(board[cellPosition.positionX - 1][cellPosition.positionY + 1]);
            }
        }
        if (cellPosition.positionX != 0)
        {
            // get left
            cells.Add(board[cellPosition.positionX - 1][cellPosition.positionY]);
        }

        if (cellPosition.positionX != board[cellPosition.positionX].Count - 1)
        {
            // get right
            cells.Add(board[cellPosition.positionX + 1][cellPosition.positionY]);
        }
        return cells;
    }
}
