using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    [SerializeField] private List<Transform> blockList;
    [SerializeField] private Transform start;
    [SerializeField] private Transform start2;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform wall2;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform rBlock;
    [SerializeField] private Transform lBlock;
    [SerializeField] private Transform mBlock;
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;

    private int init = 2;
    private Vector3 endPos;
    private Vector3 endPos2;
    private Vector3 endWall;
    private Vector3 endWall2;
    private Vector3 endMid;

    private void Awake()
    {
        endPos = start.Find("End").position + new Vector3(0, 6);
        endPos2 = start2.Find("End").position + new Vector3(0, 2);
        for (int i = 0; i < init; i++)
        {
            generateBlock();
        }
        endWall = wall.Find("End").position;
        endWall2 = wall2.Find("End").position;
        endMid = middle.Find("End").position;
    }

    private void Update()
    {
        if ((Vector3.Distance(player.position, endPos) < 100f) || (Vector3.Distance(player2.position, endPos2) < 100f))
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
        nextWall = Instantiate(lBlock, endWall, Quaternion.identity);
        endWall = nextWall.Find("End").position;
        Transform nextWall2;
        nextWall2 = Instantiate(rBlock, endWall2, Quaternion.identity);
        endWall2 = nextWall2.Find("End").position;
        Transform middleWall;
        middleWall = Instantiate(mBlock, endMid, Quaternion.identity);
        endMid = middleWall.Find("End").position;
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
        Transform part2 = generateBlock(chosen, endPos2);
        endPos = nextPart.Find("End").position + new Vector3(0, 6);
        endPos2 = part2.Find("End").position + new Vector3(0, 6);
    }
}
