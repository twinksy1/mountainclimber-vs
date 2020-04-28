using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    [SerializeField] private List<Transform> blockList;
    [SerializeField] private Transform start;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform wall2;
    [SerializeField] private Transform wBlock;
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;

    private int init = 2;
    private Vector3 endPos;
    private Vector3 endWall;
    private Vector3 endWall2;

    private void Awake()
    {
        endPos = start.Find("End").position + new Vector3(0, 6);
        for (int i = 0; i < init; i++)
        {
            generateBlock();
        }
        endWall = wall.Find("End").position;
        endWall2 = wall2.Find("End").position;
    }

    private void Update()
    {
        if ((Vector3.Distance(player.position, endPos) < 100f) || (Vector3.Distance(player2.position, endPos) < 100f))
        {
            generateBlock();
        }
        if ((Vector3.Distance(player.position, endWall) < 100f) || (Vector3.Distance(player2.position, endWall) < 100f))
        {
            generateWall();
        }
    }

    private void generateWall()
    {
        Transform nextWall;
        nextWall = Instantiate(wBlock, endWall, Quaternion.identity);
        endWall = nextWall.Find("End").position;
        Transform nextWall2;
        nextWall2 = Instantiate(wBlock, endWall2, Quaternion.identity);
        endWall2 = nextWall2.Find("End").position;
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
        endPos = nextPart.Find("End").position + new Vector3(0, 6);
    }
}
