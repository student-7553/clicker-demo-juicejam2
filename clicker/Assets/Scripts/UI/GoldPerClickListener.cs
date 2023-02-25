using UnityEngine;
using UnityEngine.UI;

// using TMPro;


public class GoldPerClickListener : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    private Text textObject;

    private void Awake()
    {
        textObject = gameObject.GetComponent(typeof(Text)) as Text;
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
