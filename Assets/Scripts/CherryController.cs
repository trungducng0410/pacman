using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private float startTime;
    private float timeCounter;
    public GameObject cherry;
    private GameObject currentCherry;
    private Vector3 startPos;
    private Vector3 endPos;
    private float duration;

    float xLeft = -16;
    float xRight = 16;
    float yBottom = -15;
    float yTop = 15;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0;
        duration = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter >= 10)
        {
            float yStart = Random.Range(yBottom, yTop);
            int xSide = Random.Range(0, 2);
            if (xSide == 0)
            {
                startPos = new Vector3(xLeft, yStart, 0);
            }
            else
            {
                startPos = new Vector3(xRight, yStart, 0);
            }
            endPos = new Vector3(-startPos.x, -startPos.y, 0);

            timeCounter = 0;

            currentCherry = Instantiate(cherry, startPos, Quaternion.identity, gameObject.transform);

            startTime = Time.time;
        }


        if (currentCherry != null)
        {
            float timeFraction = (Time.time - startTime) / duration;
            currentCherry.transform.position = Vector3.Lerp(startPos, endPos, timeFraction);

            if (currentCherry != null)
                if (currentCherry.transform.position == endPos)
                    Destroy(currentCherry);
        }

        timeCounter += Time.deltaTime;
    }
}