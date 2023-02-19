using UnityEngine;
using TMPro;

public class GoldPerClickListener : MonoBehaviour
{
    private TextMeshProUGUI textObject;

    private void Awake()
    {
        textObject = gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }

    private void FixedUpdate()
    {
        BoardManager boardManager = IdkManager.current.getBoardManager();
        if (boardManager == null)
        {
            return;
        }
        string goldPerClickString = $"{boardManager.goldPerClick}";
        if (goldPerClickString != textObject.text)
        {
            textObject.text = goldPerClickString;
        }
    }
}
