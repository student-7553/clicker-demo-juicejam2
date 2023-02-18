using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // ---------------- MAIN VAR ----------------------
    private List<List<BoardCell>> board;

    [System.NonSerialized]
    public int goldPerClick = 0;

    // private Color boardBorderColor;

    [SerializeField]
    private int _blockCount;
    public int blockCount
    {
        get { return _blockCount; }
        set
        {
            _blockCount = value;
            this.handleBlockCountBorder(_blockCount);
        }
    }

    // ---------------- Build prep ----------------------

    [System.NonSerialized]
    public bool inBuildPrep = false;
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
        board = this.generateEmptyBoard();

        // Initlize the first cell
        BoardCell centerCell = board[(int)variables.size / 2][(int)variables.size / 2];
        this.handleBlockBuild(centerCell, blocksHandler.blocks[0]);
    }

    public void resyncGoldPerClick()
    {
        int newGoldPerClick = 0;
        // apply all the stats and then add them.
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            if (!cellPosition.cell.isAlive)
            {
                continue;
            }
            newGoldPerClick = newGoldPerClick + this.getPowerValueOfCell(cellPosition);
        }

        this.goldPerClick = newGoldPerClick;
    }

    public void handleBlockBuild(BoardCell boardCell, ClassBlock level)
    {
        boardCell.initToLevel(level);
        if (this.inBuildPrep)
        {
            this.exitBuildPrep();
        }

        PlayerInfo.current.totalGold = PlayerInfo.current.totalGold - (level.goldRequirement);

        this.resyncGoldPerClick();
        this.blockCount = this.blockCount + 1;
        level.charge = level.charge - 1;
    }

    private int getPowerValueOfCell(CellWithPosition cellPosition)
    {
        if (cellPosition.cell.runeType == CellRuneType.goldPerClick)
        {
            return cellPosition.cell.powerValue;
        }

        List<CellWithPosition> adjacentCells = this.getAllAdjacentCell(cellPosition);

        if (cellPosition.cell.runeType == CellRuneType.batteryPower)
        {
            bool isAdjacentToBase = adjacentCells.Any(
                (cellPosition) => cellPosition.cell.runeType == CellRuneType.goldPerClick
            );
            return isAdjacentToBase ? cellPosition.cell.powerValue : 0;
        }
        if (cellPosition.cell.runeType == CellRuneType.boosterPower)
        {
            int value = 0;
            List<CellWithPosition> allowedCells = adjacentCells
                .Where(
                    (cellPosition) =>
                        cellPosition.cell.runeType == CellRuneType.goldPerClick
                        || cellPosition.cell.runeType == CellRuneType.batteryPower
                )
                .ToList();

            Debug.Log(allowedCells.Count);

            foreach (CellWithPosition singleCellPosition in allowedCells)
            {
                int cellPowerValue = this.getPowerValueOfCell(singleCellPosition);
                value = value + cellPowerValue;
            }
            return value;
        }

        if (cellPosition.cell.runeType == CellRuneType.boosterAllPower)
        {
            int value = 0;
            List<CellWithPosition> allowedCells = adjacentCells
                .Where(
                    (cellPosition) =>
                        cellPosition.cell.runeType == CellRuneType.goldPerClick
                        || cellPosition.cell.runeType == CellRuneType.batteryPower
                        || cellPosition.cell.runeType == CellRuneType.boosterPower
                )
                .ToList();

            foreach (CellWithPosition singleCellPosition in allowedCells)
            {
                int cellPowerValue = this.getPowerValueOfCell(singleCellPosition);
                value = cellPowerValue + cellPowerValue;
            }
            return value;
        }
        return 0;
    }

    public void enterBuildPrep(ClassBlock prepLevel)
    {
        if (this.inBuildPrep == true)
        {
            return;
        }
        this.inBuildPrep = true;
        this.buildLevel = prepLevel.level;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.enterBuildPrep();
        }
    }

    public void exitBuildPrep()
    {
        if (this.inBuildPrep == false)
        {
            return;
        }
        this.inBuildPrep = false;
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

        foreach (CellWithPosition cellPosition in getAllCells())
        {
            if (!cellPosition.cell.isAlive)
            {
                continue;
            }
            Debug.Log(percentage);
            cellPosition.cell.changeBorderColor(percentage);
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

    private List<List<BoardCell>> generateEmptyBoard()
    {
        List<List<BoardCell>> emptyBoard = new List<List<BoardCell>>();
        for (int columnIndex = 0; columnIndex < variables.size; columnIndex++)
        {
            List<BoardCell> emptyRow = new List<BoardCell>();
            for (int rowIndex = 0; rowIndex < variables.size; rowIndex++)
            {
                Vector3 cellSpawnPostion = this.getCellPosition(rowIndex, columnIndex);

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
    }

    private Vector3 getCellPosition(int rowIndex, int columnIndex)
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

    private List<CellWithPosition> getAllAdjacentCell(CellWithPosition cellPosition)
    {
        List<CellWithPosition> cells = new List<CellWithPosition>();
        if (cellPosition.positionY != 0)
        {
            cells.Add(
                new CellWithPosition(
                    board[cellPosition.positionY - 1][cellPosition.positionX],
                    cellPosition.positionY - 1,
                    cellPosition.positionX
                )
            );

            cells.Add(
                cellPosition.positionY % 2 == 0
                    ? new CellWithPosition(
                        board[cellPosition.positionY - 1][cellPosition.positionX + 1],
                        cellPosition.positionY - 1,
                        cellPosition.positionX + 1
                    )
                    : new CellWithPosition(
                        board[cellPosition.positionY - 1][cellPosition.positionX - 1],
                        cellPosition.positionY - 1,
                        cellPosition.positionX - 1
                    )
            );
        }
        if (cellPosition.positionY != board.Count - 1)
        {
            cells.Add(
                new CellWithPosition(
                    board[cellPosition.positionY + 1][cellPosition.positionX],
                    cellPosition.positionY + 1,
                    cellPosition.positionX
                )
            );

            cells.Add(
                cellPosition.positionY % 2 == 0
                    ? new CellWithPosition(
                        board[cellPosition.positionY + 1][cellPosition.positionX + 1],
                        cellPosition.positionY + 1,
                        cellPosition.positionX + 1
                    )
                    : new CellWithPosition(
                        board[cellPosition.positionY + 1][cellPosition.positionX - 1],
                        cellPosition.positionY + 1,
                        cellPosition.positionX - 1
                    )
            );
        }
        if (cellPosition.positionX != 0)
        {
            cells.Add(
                new CellWithPosition(
                    board[cellPosition.positionY][cellPosition.positionX - 1],
                    cellPosition.positionY,
                    cellPosition.positionX - 1
                )
            );
        }

        if (cellPosition.positionX != board[cellPosition.positionX].Count - 1)
        {
            cells.Add(
                new CellWithPosition(
                    board[cellPosition.positionY][cellPosition.positionX + 1],
                    cellPosition.positionY,
                    cellPosition.positionX + 1
                )
            );
        }
        return cells.Where((cellPosition) => cellPosition.cell.isAlive).ToList();
    }

    private void OnDestroy()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.clearBoardManager();
        }
    }
}
