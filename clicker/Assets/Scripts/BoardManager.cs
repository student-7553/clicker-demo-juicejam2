using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private List<List<BoardCell>> board;

    [SerializeField]
    private GameObject boardCellPrefab;

    // Temp varaible delete/replace later
    private Vector2 tempAnchorPoint = new Vector2(-4.5f, -4.5f);

    private int size;

    private void Start()
    {
        size = 9;

        // Don't have save file
        board = this.generateBoard();
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
            Vector3 cellSpawnPostion = new Vector3(
                tempAnchorPoint.x + rowIndex,
                tempAnchorPoint.y + columnIndex,
                0f
            );
            return cellSpawnPostion;
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
