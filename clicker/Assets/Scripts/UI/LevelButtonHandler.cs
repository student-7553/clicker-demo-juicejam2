using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelButtonHandler : MonoBehaviour
{
    private ClassBlock level;
    private Action<ClassBlock> onlevelClick;
    private BoardManager boardManager;

    public void OnClick()
    {
        if (level == null)
        {
            return;
        }
        onlevelClick.Invoke(level);
    }

    public void initlize(ClassBlock _level, Action<ClassBlock> _onLevelClick)
    {
        level = _level;
        onlevelClick = _onLevelClick;
        TextMeshProUGUI buttonText =
            gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        this.boardManager = IdkManager.current.getBoardManager();
        if (boardManager == null)
        {
            throw new Exception("boardManager is null");
        }
        buttonText.text = level.name + "/" + level.goldRequirement;
    }

    private void FixedUpdate()
    {
        handleGoldCompute();
    }

    private void handleGoldCompute()
    {
        if (level == null || PlayerInfo.current == null)
        {
            return;
        }
        if (PlayerInfo.current.totalGold >= (level.goldRequirement) && level.charge > 0)
        {
            Button buttonObject = GetComponent(typeof(Button)) as Button;
            buttonObject.interactable = true;
        }
        else
        {
            Button buttonObject = GetComponent(typeof(Button)) as Button;
            buttonObject.interactable = false;
        }
    }
}
