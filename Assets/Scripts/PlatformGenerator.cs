using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generates the platforms to jump on
 * 
 * Distance all blocks should be vertically: 4.46
 * Horizontal Zone Range (X-Values): -10.58 to 10.58
 * 
 */
public class PlatformGenerator : MonoBehaviour {

    public GameObject platformPrefab;
    public float verticalMin = 0F, verticalMax = 7.75F;
    public float horizontalMin = -10.58F, horizontalMax = 10.58F;
    public float lastVerticalPosition = 0F;
    public int numOfBlocksPerRowMin = 1, numOfBlocksPerRowMax = 3;
    public int numOfBlocksPresent = 0, maxNumOfBlocksPresent = 200;
    public bool spawningPlatforms = false;

    // Edges of Box
    public GameObject leftEdge, rightEdge;
    public float edgeIncreaseValue = 200F;

    void Start()
    {
        spawningPlatforms = true;
        StartCoroutine(SpawnPlatforms());
    }

    void Update()
    {
        if (!spawningPlatforms && numOfBlocksPresent < 100)
        {
            StartCoroutine(SpawnPlatforms());
        }
    }

    // Spawns Platforms using a combination of a while-loop and for-loop
    // for-loop is used to spawn platforms in a row
    IEnumerator SpawnPlatforms()
    {
        //int numOfBlocksInRow = Random.Range(numOfBlocksPerRowMin, numOfBlocksPerRowMax);

        while(numOfBlocksPresent < maxNumOfBlocksPresent)
        {
            yield return null;
            
            Vector3 newBlockPosition = new Vector3();
            
            for (int i = 0; i < 3/*numOfBlocksInRow*/; i++)
            {
                yield return null;
                newBlockPosition.y = lastVerticalPosition + verticalMax;
                newBlockPosition.x = Random.Range(horizontalMin, horizontalMax);
                Instantiate(platformPrefab, newBlockPosition, new Quaternion());
                numOfBlocksPresent++;
            }

            lastVerticalPosition = lastVerticalPosition + verticalMax;
        }

        Vector2 oldLeftEdgeSize = leftEdge.GetComponent<SpriteRenderer>().size;
        Vector2 oldRightEdgeSize = leftEdge.GetComponent<SpriteRenderer>().size;
        leftEdge.GetComponent<SpriteRenderer>().size = new Vector2(4.170658F, oldLeftEdgeSize.y + edgeIncreaseValue);
        rightEdge.GetComponent<SpriteRenderer>().size = new Vector2(4.170658F, oldRightEdgeSize.y + edgeIncreaseValue);

        spawningPlatforms = false;
    }

}
