using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    private SingleLevel level;

    public void OnClick()
    {
        // Todo:
    }

    public void initlize(SingleLevel _level)
    {
        level = _level;
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
        if (level.level == 0 || PlayerInfo.current == null)
        {
            return;
        }

        if (PlayerInfo.current.playerStats.totalGold > level.scoreRequirement)
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
