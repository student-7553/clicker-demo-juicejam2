using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // ---------------- MAIN VAR ----------------------
    private List<List<BoardCell>> board;

    [System.NonSerialized]
    public int goldPerClick = 0;

    [System.NonSerialized]
    public int highestLevel = 0;

    // ---------------- Build prep ----------------------

    [System.NonSerialized]
    public bool inPhase = false;

    public BoardPhases phase;

    [System.NonSerialized]
    public int buildLevel;

    // ---------------- OUTSIDE  ----------------------
    [SerializeField]
    private GameObject boardCellPrefab;

    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private Variables variables;

    private void Start()
    {
        phase = BoardPhases.normal;
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

    public void handleBlockBuild(BoardCell cell, ClassBlock block)
    {
        cell.initToBlock(block);
        if (this.phase == BoardPhases.build)
        {
            this.exitPhases();
        }

        PlayerInfo.current.totalGold = PlayerInfo.current.totalGold - (block.goldRequirement);

        this.resyncGoldPerClick();
        this.handleBorderSync();
        block.charge = block.charge - 1;

        if (block.level > this.highestLevel)
        {
            this.highestLevel = block.level;
        }
    }

    public void handleBlockDestroy(BoardCell cell)
    {
        ClassBlock block = cell.currentBlock;
        block.charge = block.charge + 1;
        cell.destroyCurrentBlock();
        this.resyncGoldPerClick();

        if (this.phase == BoardPhases.destroy)
        {
            this.exitPhases();
        }
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

    public void enterBuildPhase(ClassBlock prepLevel)
    {
        if (this.phase != BoardPhases.normal)
        {
            return;
        }
        this.phase = BoardPhases.build;
        this.buildLevel = prepLevel.level;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.changePhase(BoardPhases.build);
        }
    }

    public void enterDestoryPhase()
    {
        if (this.phase != BoardPhases.normal)
        {
            return;
        }
        this.phase = BoardPhases.destroy;

        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.changePhase(BoardPhases.destroy);
        }
    }

    public void exitPhases()
    {
        if (this.phase == BoardPhases.normal)
        {
            return;
        }
        this.phase = BoardPhases.normal;
        foreach (CellWithPosition cellPosition in getAllCells())
        {
            cellPosition.cell.changePhase(BoardPhases.normal);
        }
    }

    private void handleBorderSync()
    {
        float percentage = this.goldPerClick / variables.maxiumPowerPerClick;

        foreach (CellWithPosition cellPosition in getAllCells())
        {
            if (!cellPosition.cell.isAlive)
            {
                continue;
            }
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

    private void Awake()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.registerBoardManager(this);
        }
    }

    private void OnDestroy()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.clearBoardManager();
        }
    }
}
