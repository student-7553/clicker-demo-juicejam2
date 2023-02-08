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

    // private SingleLevel currenLevel;

    private int size;

    private void Start()
    {
        size = 9;

        // Don't have save file
        board = this.generateBoard();
        this.initFirstCenterCell();
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
                    Quaternion.identity
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

    private void Awake()
    {
        if (SceneManagerManager.current != null)
        {
            SceneManagerManager.current.mainGameLocalManagers.boardManager = this;
        }
    }

    private void initFirstCenterCell()
    {
        BoardCell centerCell = board[(int)size / 2][(int)size / 2];
        centerCell.init(levelsHandler.levels[0]);
    }

    private void OnDestroy()
    {
        if (SceneManagerManager.current != null)
        {
            SceneManagerManager.current.mainGameLocalManagers.boardManager = null;
        }
    }
}
