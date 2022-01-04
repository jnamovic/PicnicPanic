using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntDisplay : MonoBehaviour
{
    public GameObject antImage;
    public float space;
    public GameObject parentToSet;
    private List<GameObject> antSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        antSprites = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayAnts(int numAnts)
    {
        if (numAnts == -1)
        {
            foreach (GameObject ant in antSprites)
            {
                Destroy(ant);
            }
            antSprites = new List<GameObject>();
            return;
        }

        if (numAnts % 2 == 0)
        {
            for (int i = 0; i < numAnts / 2; i++)
            {
                GameObject newAntR = Instantiate(antImage);
                GameObject newAntL = Instantiate(antImage);
                float xPosR = (space * i) + (space / 2.0f);
                float xPosL = -xPosR;
                newAntR.transform.position = new Vector3(transform.position.x + xPosR, transform.position.y, transform.position.z);
                newAntL.transform.position = new Vector3(transform.position.x + xPosL, transform.position.y, transform.position.z);
                newAntR.transform.SetParent(parentToSet.transform);
                newAntL.transform.SetParent(parentToSet.transform);
                antSprites.Add(newAntR);
                antSprites.Add(newAntL);
            }
        }
        else
        {
            for (int i = -(numAnts/2); i < numAnts/2; i++)
            {
                GameObject newAnt = Instantiate(antImage);
                newAnt.transform.SetParent(parentToSet.transform);
                float xPos = space * i;
                newAnt.transform.position = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z);
                antSprites.Add(newAnt);
            }
        }
    }
}
