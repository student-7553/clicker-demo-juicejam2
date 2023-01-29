using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private List<List<BoardCell>> board;

    private int size;

    void Start()
    {
        size = 9;

        // Don't have save file
        board = this.generateEmptyBoard();
    }

    private List<List<BoardCell>> generateEmptyBoard()
    {
        List<List<BoardCell>> emptyBoard = new List<List<BoardCell>>();
        for (int columnIndex = 0; columnIndex < size; columnIndex++)
        {
            List<BoardCell> emptyRow = new List<BoardCell>();
            for (int rowIndex = 0; rowIndex < size; rowIndex++)
            {
                emptyRow.Add(null);
            }
            emptyBoard.Add(emptyRow);
        }
        return emptyBoard;
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
