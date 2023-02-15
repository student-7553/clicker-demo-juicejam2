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
            this.nextBlockPrice = (_blockCount + 1) * variables.blockIncrease;
        }
    }

    public int nextBlockPrice;

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

        this.nextBlockPrice = variables.blockIncrease * 1;

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
            newGoldPerClick = newGoldPerClick + cellPosition.cell.runeStats.goldPerClickIncease;
        }
        goldPerClick = newGoldPerClick;
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
        float maxLevels = 100f;
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
}
