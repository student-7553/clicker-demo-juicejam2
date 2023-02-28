using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardPlayerManager : MonoBehaviour
{
    private List<List<BoardCell>> board;

    private LayerMask clickableLayerMask;

    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private ClickCombo clickCombo;

    [SerializeField]
    private Blocks blocks;

    [SerializeField]
    private Variables variables;

    [SerializeField]
    private GameObject tickTextPrefab;

    [SerializeField]
    private GameObject clickParticlePrefab;

    private void Start()
    {
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

            SoundEffectManager.current.triggerSoundEffect(GameSoundEffects.ON_BUILD);
            this.boardManager.handleBlockBuild(boardCell, block);

            return;
        }

        if (!boardCell.isAlive)
        {
            return;
        }
        this.handleTick(boardCell);
    }

    public void handleTick(BoardCell boardCell)
    {
        int effectiveGoldPerClick = this.boardManager.goldPerClick;

        if (this.clickCombo.clickCombo > variables.comboDoubleThreshhold)
        {
            effectiveGoldPerClick = effectiveGoldPerClick * 2;
        }

        PlayerInfo.current.totalGold = PlayerInfo.current.totalGold + effectiveGoldPerClick;

        SoundEffectManager.current.triggerSoundEffect(GameSoundEffects.ON_TICK);
        boardCell.onTick();

        // Instantiate(clickParticlePrefab, boardCell.transform);

        this.clickCombo.handlePlayerTick();
        this.handleTickTextSpawn(boardCell);
    }

    private void handleTickTextSpawn(BoardCell boardCell)
    {
        Vector3 spawnPosition = boardCell.transform.position;
        spawnPosition.x = spawnPosition.x + 0.25f;

        float randomXAdjust = Random.Range(-0.5f, 0.5f);

        spawnPosition.x = spawnPosition.x + randomXAdjust;
        spawnPosition.y = spawnPosition.y + 0.2f;
        spawnPosition.z = 1f;

        GameObject tickTextObject = Instantiate(tickTextPrefab, spawnPosition, Quaternion.identity);

        TickTextHandler tickTextHandler =
            tickTextObject.GetComponent(typeof(TickTextHandler)) as TickTextHandler;

        tickTextHandler.init(this.boardManager.goldPerClick);
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
