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

    public void handlePlayerClick()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40, clickableLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        BoardCell boardCell = hit.collider.gameObject.GetComponent(typeof(BoardCell)) as BoardCell;
        if (boardCell == null)
        {
            return;
        }

        if (this.boardManager.phase == BoardPhases.destroy)
        {
            if (!boardCell.isAlive)
            {
                return;
            }
            this.boardManager.handleBlockDestroy(boardCell);
        }

        if (this.boardManager.phase == BoardPhases.build)
        {
            if (boardCell.isAlive)
            {
                return;
            }
            ClassBlock block = this.blocks.getBlock(this.boardManager.buildLevel);
            this.boardManager.handleBlockBuild(boardCell, block);
            return;
        }

        if (!boardCell.isAlive)
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

    private void Awake()
    {
        if (IdkManager.current != null)
        {
            IdkManager.current.registerBoardPlayerManager(this);
        }
    }

    private void OnDestroy()
    {
        PlayerInput.current.leftMouseClickEvent =
            PlayerInput.current.leftMouseClickEvent - handlePlayerClick;

        if (IdkManager.current != null)
        {
            IdkManager.current.clearBoardPlayerManager();
        }
    }
}
