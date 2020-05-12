using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{

    //JL 3-20: added the game start instantiation
    //JL 4-20: added generation on update
    //JL 4-26-20: added distance checking for wall/platform generation
    //JL 4-28-20: added wall generation and 2nd player platforms

    //List of prefabs to generate platforms
    [SerializeField] private List<Transform> blockList;
    //The two starting prefabs
    [SerializeField] private Transform start;
    [SerializeField] private Transform start2;
    //The three starting walls
    [SerializeField] private Transform wall;
    [SerializeField] private Transform wall2;
    [SerializeField] private Transform middle;
    //The prefabs used to generate walls
    [SerializeField] private Transform rBlock;
    [SerializeField] private Transform lBlock;
    [SerializeField] private Transform mBlock;
    //Players
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;

    //number of block immediately generated
    private int init = 2;

    //end positions of each of the 5 block types (p1, p2, 3 walls)
    private Vector3 endPos;
    private Vector3 endPos2;
    private Vector3 endWall;
    private Vector3 endWall2;
    private Vector3 endMid;
    public int currentTime, interval = 13, stage = 1;

    private void Awake()
    {
        //finds the end positions of the two starting blocks
        endPos = start.Find("End").position + new Vector3(0, 6);
        endPos2 = start2.Find("End").position + new Vector3(0, 2);
        //generate two more blocks
        for (int i = 0; i < init; i++)
        {
            generateBlock();
        }
        //finds end positions of the wall
        endWall = wall.Find("End").position;
        endWall2 = wall2.Find("End").position;
        endMid = middle.Find("End").position;
    }

    //checks player locations relative to current end points and generate walls/platforms accordingly
    private void Update()
    {
        currentTime = (int)Time.timeSinceLevelLoad;
        if(currentTime > interval)
        {
            if (stage < 3)
            {
                stage += 1;
            }
            interval += 10;
        }
        if ((Vector3.Distance(player.position, endPos) < 100f) || (Vector3.Distance(player2.position, endPos2) < 100f))
        {
            generateBlock();
        }
        if ((Vector3.Distance(player.position, endWall) < 100f) || (Vector3.Distance(player2.position, endWall2) < 100f))
        {
            generateWall();
        }
    }


    //generates new walls and grabs the new end points
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

    //instantiates and returns the new platform
    private Transform generateBlock(Transform block, Vector3 position)
    {
        Transform nextPart;
        nextPart = Instantiate(block, position, Quaternion.identity);
        return nextPart;
    }

    //generates new platforms and grabs the new end points
    private void generateBlock()
    {
        Transform chosen = blockList[Random.Range(0, blockList.Count)];
        Transform nextPart = generateBlock(chosen, endPos);
        Transform part2 = generateBlock(chosen, endPos2);
        endPos = nextPart.Find("End").position + new Vector3(0, 6);
        endPos2 = part2.Find("End").position + new Vector3(0, 6);
    }
}
