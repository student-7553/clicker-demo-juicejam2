using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using TMPro;
// using UnityEngine.UI;

public class LevelBoardhandler : MonoBehaviour
{
    [SerializeField]
    private Levels levelsHandler;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<GameObject> buttons = new List<GameObject>();

    private int initYPosition = 35;

    void Start()
    {
        int index = 0;
        foreach (SingleClassLevel level in levelsHandler.trueLevels)
        {
            index++;
            GameObject buttonObject = Instantiate(buttonPrefab, this.transform);
            buttons.Add(buttonObject);

            LevelButtonHandler buttonHandler =
                buttonObject.GetComponent(typeof(LevelButtonHandler)) as LevelButtonHandler;
            buttonHandler.initlize(level, onLevelClick);

            buttonObject.transform.localPosition = new Vector3(0, (-50 * index) - initYPosition, 0);
        }
    }

    public void onLevelClick(SingleClassLevel level)
    {
        BoardManager boardManager = SceneManagerManager.current.getBoardManager();
        boardManager.enterBuildPrep(level);
    }

    private void Awake()
    {
        if (SceneManagerManager.current == null)
        {
            return;
        }

        // boardManager = SceneManagerManager.current.mainGameLocalManagers.boardManager;
    }
}
