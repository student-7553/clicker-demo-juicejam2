using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardPlayerManager : MonoBehaviour
{
    private List<List<BoardCell>> board;

    private LayerMask clickableLayerMask;

    private BoardManager boardManager;

    [SerializeField]
    private Blocks blocks;

    private void Start()
    {
        if (IdkManager.current == null)
        {
            throw new System.Exception("IdkManager is not inilized");
        }

        this.boardManager = IdkManager.current.getBoardManager();
        clickableLayerMask = LayerMask.GetMask("Clickable");

        PlayerInput.current.leftMouseClickEvent =
            PlayerInput.current.leftMouseClickEvent + handlePlayerClick;
    }

    private void OnDestroy()
    {
        if (PlayerInput.current == null)
        {
            return;
        }
        PlayerInput.current.leftMouseClickEvent =
            PlayerInput.current.leftMouseClickEvent - handlePlayerClick;
    }

    public void handlePlayerClick()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40, clickableLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        bool isCell = hit.collider.gameObject.CompareTag("Cell");
        if (!isCell)
        {
            return;
        }

        if (this.boardManager.inBuildPrep)
        {
            BoardCell boardCell =
                hit.collider.gameObject.GetComponent(typeof(BoardCell)) as BoardCell;
            if (boardCell == null || boardCell.isAlive)
            {
                return;
            }
            ClassBlock block = this.blocks.getBlock(this.boardManager.buildLevel);
            this.boardManager.handleBlockBuild(boardCell, block);
            return;
        }

        this.handleClick(hit.collider.gameObject);
    }

    private void handleClick(GameObject cellGameObject)
    {
        BoardCell boardCell = cellGameObject.GetComponent(typeof(BoardCell)) as BoardCell;
        if (boardCell == null || !boardCell.isAlive)
        {
            return;
        }
        this.handleTick();
    }

    public void handleTick()
    {
        PlayerInfo.current.totalGold =
            PlayerInfo.current.totalGold + this.boardManager.goldPerClick;
    }
}
