using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeBoardhandler : MonoBehaviour
{
    [SerializeField]
    private Levels levelsHandler;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<GameObject> buttons = new List<GameObject>();

    private int initYPosition = 35;

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (SingleLevel level in levelsHandler.levels)
        {
            index++;
            GameObject buttonObject = Instantiate(buttonPrefab, this.transform);
            buttons.Add(buttonObject);

            LevelButtonHandler buttonHandler =
                buttonObject.GetComponent(typeof(LevelButtonHandler)) as LevelButtonHandler;

            buttonHandler.initlize(level);

            buttonObject.transform.localPosition = new Vector3(0, (-50 * index) - initYPosition, 0);
        }
    }

    private void onLevelClick() { }

    // private void FixedUpdate()
    // {
    //     handleGoldCompute();
    // }

    // private void handleGoldCompute()
    // {
    //     if (PlayerInfo.current == null || buttons.Count == 0)
    //     {
    //         return;
    //     }
    //     for (int index = 0; index < buttons.Count; index++)
    //     {
    //         if (
    //             PlayerInfo.current.playerStats.totalGold
    //             > levelsHandler.levels[index].scoreRequirement
    //         )
    //         {
    //             Button buttonObject = buttons[index].GetComponent(typeof(Button)) as Button;
    //             buttonObject.interactable = true;
    //         }
    //     }
    // }
}
