
﻿// GeneratorDouble.cs - This is what controls the generation of walls for the player(s). Our platforms are procedurally generated and this is the logic for it.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDouble : MonoBehaviour
{

    //JL 3-4-20: added the game start instantiation
    //JL 3-30-20: added generation on update
    //JL 4-19-20: small fixes
    //JL 4-26-20: added distance checking for wall/platform generation, blocks now spawn infinitely
    //JL 4-28-20: added wall generation and 2nd player platforms
    //JL 4-29-20: modified to support a second player and its platforms
    //JL 5-11-20: added counter, variable, and functionality for platform biome change
    //JL 5-11-20: implemented settingsmanager from game menu to import unique terrain boolean
    //AM 5-16-20: updated file description

    //Lists of prefabs to generate platforms
    [SerializeField] private List<Transform> blockList;
    [SerializeField] private List<Transform> rockList;
    [SerializeField] private List<Transform> iceList;
    [SerializeField] private List<Transform> currentList;
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
    [SerializeField] private Transform rRock;
    [SerializeField] private Transform lRock;
    [SerializeField] private Transform mRock;
    [SerializeField] private Transform rIce;
    [SerializeField] private Transform lIce;
    [SerializeField] private Transform mIce;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private Transform mid;
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

    //variables tracking and concerning game length
    public int currentTime, interval = 13, stage = 1, counter = 0;

    //for generation settings
    public bool unique = false;
    GameObject settings;

    private void Awake()
    {
        //Finds the settingsmanager object and its 'uniqueOn' variable
        settings = GameObject.Find("SettingsManager");
        SettingsManager uniqueToggle = settings.GetComponent<SettingsManager>();
        unique = uniqueToggle.uniqueOn;
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
        if(counter > 3)
        {
            if (stage < 3)
            {
                stage += 1;
            }
            counter = 0;
        }
        if ((Vector3.Distance(player.position, endPos) < 100f) || (Vector3.Distance(player2.position, endPos2) < 100f))
        {
            generateBlock();
            counter += 1;
        }
        if ((Vector3.Distance(player.position, endWall) < 100f) || (Vector3.Distance(player2.position, endWall2) < 100f))
        {
            generateWall();
        }
    }


    //generates new walls and grabs the new end points
    private void generateWall()
    {
        if (stage == 1)
        {
            left = lBlock;
            right = rBlock;
            mid = mBlock;
        }
        else if(stage == 2)
        {
            left = lRock;
            right = rRock;
            mid = mRock;
        }
        else
        {
            left = lIce;
            right = rIce;
            mid = mIce;
        }
        Transform nextWall;
        nextWall = Instantiate(left, endWall, Quaternion.identity);
        endWall = nextWall.Find("End").position;
        Transform nextWall2;
        nextWall2 = Instantiate(right, endWall2, Quaternion.identity);
        endWall2 = nextWall2.Find("End").position;
        Transform middleWall;
        middleWall = Instantiate(mid, endMid, Quaternion.identity);
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
        if (stage == 1)
        {
            currentList = blockList;
        }
        else if (stage == 2)
        {
            currentList = rockList;
        }
        else
        {
            currentList = iceList;
        }
        Transform chosen1 = currentList[Random.Range(0, currentList.Count)];
        Transform chosen2;
        if (unique)
        {
            chosen2 = currentList[Random.Range(0, currentList.Count)];
        }
        else
        {
            chosen2 = chosen1;
        }
        Transform nextPart = generateBlock(chosen1, endPos);
        Transform part2 = generateBlock(chosen2, endPos2);
        endPos = nextPart.Find("End").position + new Vector3(0, 6);
        endPos2 = part2.Find("End").position + new Vector3(0, 6);
    }
}
