using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardhandler : MonoBehaviour
{
    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private BoardManager boardManager;

    private List<GameObject> buttons = new List<GameObject>();

    private int initYPosition = 35;

    void Start()
    {
        int index = 0;

        RectTransform prefabRectTransform =
            buttonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;

        foreach (ClassBlock level in blocksHandler.blocks)
        {
            index++;
            GameObject buttonObject = Instantiate(buttonPrefab, this.transform);
            buttons.Add(buttonObject);

            BlockButtonHandler buttonHandler =
                buttonObject.GetComponent(typeof(BlockButtonHandler)) as BlockButtonHandler;

            buttonHandler.initlize(level, onLevelClick, this.boardManager);

            float topMargin = prefabRectTransform.rect.height;

            buttonObject.transform.localPosition = new Vector3(0, (-topMargin * index), 0);
        }
    }

    public void onLevelClick(ClassBlock level)
    {
        this.boardManager.enterBuildPhase(level);
    }
}
