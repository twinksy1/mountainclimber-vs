using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    [SerializeField] private List<Transform> blockList;
    [SerializeField] private Transform start;

    private int gameLength = 8;
    private Vector3 endPos;

    private void Awake()
    {
        endPos = start.Find("End").position;
        for (int i = 0; i < gameLength; i++)
        {
            generateBlock();
        }
    }

    private Transform generateBlock(Transform block, Vector3 position)
    {
        Transform nextPart;
        nextPart = Instantiate(block, position, Quaternion.identity);
        return nextPart;
    }

    private void generateBlock()
    {
        Transform chosen = blockList[Random.Range(0, blockList.Count)];
        Transform nextPart = generateBlock(chosen, endPos);
        endPos = nextPart.Find("End").position;
    }
}
