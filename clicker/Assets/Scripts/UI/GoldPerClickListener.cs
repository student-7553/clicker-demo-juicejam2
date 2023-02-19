using UnityEngine;
using TMPro;

public class GoldPerClickListener : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    private TextMeshProUGUI textObject;

    private void Awake()
    {
        textObject = gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }

    private void FixedUpdate()
    {
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
