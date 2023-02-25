using UnityEngine;
using UnityEngine.UI;

public class TotalGoldListener : MonoBehaviour
{
    private Text totalScoreText;

    private void Awake()
    {
        totalScoreText = gameObject.GetComponent(typeof(Text)) as Text;
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
