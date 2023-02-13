using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelButtonHandler : MonoBehaviour
{
    private SingleClassLevel level;
    private Action<SingleClassLevel> onlevelClick;

    public void OnClick()
    {
        if (level == null)
        {
            return;
        }
        onlevelClick.Invoke(level);
    }

    public void initlize(SingleClassLevel _level, Action<SingleClassLevel> _onLevelClick)
    {
        level = _level;
        onlevelClick = _onLevelClick;
        TextMeshProUGUI buttonText =
            gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        buttonText.text = level.name;
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

        if (PlayerInfo.current.playerStats.totalGold >= level.goldRequirement)
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
