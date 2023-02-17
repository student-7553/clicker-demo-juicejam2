using UnityEngine;
using TMPro;

public class TotalGoldListener : MonoBehaviour
{
    private TextMeshProUGUI totalScoreText;

    private void Awake()
    {
        totalScoreText = gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }

    private void FixedUpdate()
    {
        if (PlayerInfo.current == null)
        {
            return;
        }
        string currentTotalGoldString = $"{PlayerInfo.current.totalGold}";

        if (currentTotalGoldString != totalScoreText.text)
        {
            totalScoreText.text = currentTotalGoldString;
        }
    }
}
