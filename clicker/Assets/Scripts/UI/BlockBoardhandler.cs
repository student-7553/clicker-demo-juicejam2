using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardhandler : MonoBehaviour
{
    private BoardManager boardManager;

    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<GameObject> buttons = new List<GameObject>();

    private int initYPosition = 35;

    void Start()
    {
        if (IdkManager.current == null)
        {
            throw new System.Exception("IdkManager is not inilized");
        }

        this.boardManager = IdkManager.current.getBoardManager();

        int index = 0;
        foreach (ClassBlock level in blocksHandler.blocks)
        {
            index++;
            GameObject buttonObject = Instantiate(buttonPrefab, this.transform);
            buttons.Add(buttonObject);

            BlockButtonHandler buttonHandler =
                buttonObject.GetComponent(typeof(BlockButtonHandler)) as BlockButtonHandler;
            buttonHandler.initlize(level, onLevelClick);

            buttonObject.transform.localPosition = new Vector3(0, (-50 * index) - initYPosition, 0);
        }
    }

    public void onLevelClick(ClassBlock level)
    {
        this.boardManager.enterBuildPrep(level);
    }
}
