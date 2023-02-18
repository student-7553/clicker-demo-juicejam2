using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BlockButtonHandler : MonoBehaviour
{
    private ClassBlock level;
    private Action<ClassBlock> onlevelClick;
    private BoardManager boardManager;
    private TextMeshProUGUI buttonText;

    public void OnClick()
    {
        if (level == null)
        {
            return;
        }
        this.onlevelClick.Invoke(level);
    }

    public void initlize(ClassBlock _level, Action<ClassBlock> _onLevelClick)
    {
        this.level = _level;
        this.onlevelClick = _onLevelClick;
        this.buttonText =
            gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        this.boardManager = IdkManager.current.getBoardManager();
        if (boardManager == null)
        {
            throw new Exception("boardManager is null");
        }
        this.buttonText.text = level.name + "/" + level.goldRequirement + "/" + level.charge;
    }

    private void FixedUpdate()
    {
        handleCompute();
    }

    private void handleCompute()
    {
        if (level == null || PlayerInfo.current == null)
        {
            return;
        }
        Button buttonObject = GetComponent(typeof(Button)) as Button;

        bool isInteractable =
            PlayerInfo.current.totalGold >= (level.goldRequirement) && level.charge > 0;
        buttonObject.interactable = isInteractable;

        buttonText.text = this.level.name + "/" + level.goldRequirement + "/" + level.charge;
    }
}
