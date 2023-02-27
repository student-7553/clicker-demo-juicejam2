using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BlockButtonHandler : MonoBehaviour
{
    //  ------------------------- child components -------------------------
    [SerializeField]
    private GameObject chargeTextObject;

    private TextMeshProUGUI chargeText;

    [SerializeField]
    private GameObject priceTextObject;

    private TextMeshProUGUI priceText;

    [SerializeField]
    private GameObject runeImageObject;

    private Image runeImage;

    [SerializeField]
    private GameObject typeTextObject;

    private TextMeshProUGUI typeText;

    private ClassBlock block;
    private Action<ClassBlock> onlevelClick;
    private TextMeshProUGUI buttonText;
    private BoardManager boardManager;
    private Button buttonObject;

    private bool isObfuscated;

    public void OnClick()
    {
        if (block == null)
        {
            return;
        }
        this.onlevelClick.Invoke(block);
    }

    public void initlize(
        ClassBlock _level,
        Action<ClassBlock> _onLevelClick,
        BoardManager _boardManager
    )
    {
        this.boardManager = _boardManager;
        this.block = _level;
        this.onlevelClick = _onLevelClick;
        this.buttonText =
            gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        this.buttonObject = GetComponent(typeof(Button)) as Button;
        this.buttonText.text = block.name + "/" + block.goldRequirement + "/" + block.charge;
        this.isObfuscated = false;

        this.chargeText = chargeTextObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        this.priceText = priceTextObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        this.runeImage = runeImageObject.GetComponent(typeof(Image)) as Image;
        this.typeText = typeTextObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;

        this.runeImage.sprite = this.block.sprite;
        this.typeText.text = $"{this.block.name}";
    }

    private void FixedUpdate()
    {
        this.handleState();
        this.handleText();
    }

    private void handleText()
    {
        if (this.isObfuscated)
        {
            this.chargeText.text = "";
            this.priceText.text = "";
        }
        else
        {
            this.chargeText.text = $"{this.block.charge}";
            this.priceText.text = $"{this.block.goldRequirement}";
        }
    }

    private void handleState()
    {
        if (block == null || PlayerInfo.current == null)
        {
            return;
        }

        this.isObfuscated = (this.boardManager.highestLevel + 1) < block.level;
        buttonObject.interactable = this.getIsInteractable();
    }

    private bool getIsInteractable()
    {
        if (this.isObfuscated)
        {
            return false;
        }
        return PlayerInfo.current.totalGold >= (block.goldRequirement) && block.charge > 0;
    }
}
