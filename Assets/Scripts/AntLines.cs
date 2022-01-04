using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLines : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ant;
    public GameObject Target;
    public float speed = 1.0f;
    public int numberOfAntLines = 3;
    public int numberOfAntsPerLine = 10;
    private List<List<GameObject>> lines = new List<List<GameObject>>();

    public void Start()
    {
        List<GameObject> ants;

        for (int lineNum = 0; lineNum < numberOfAntLines; lineNum++)
        {
            ants = new List<GameObject>();
            float antX = getAntX(lineNum);

            for (int antNum = 0; antNum < numberOfAntsPerLine; antNum++)
            {

                float antZ = getAntZ(lineNum, antNum);
                ants.Add(createAnt(antX, antZ));
            }

            lines.Add(ants);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        float antX;
        float antZ;
        GameObject ant;
        int lastAnt;
        for (int lineNum = 0; lineNum < lines.Count; lineNum++)
        {

            antX = getAntX(lineNum);

            for (int antNum = 0; antNum < lines[lineNum].Count; antNum++)
            {

                antZ = getAntZ(lineNum, antNum);
                ant = lines[lineNum][antNum];
                lastAnt = lines[lineNum].Count + 1;
                if (ant.name.Equals("ThrownAnt"))
                {
                    lines[lineNum].Remove(ant);
                    lines[lineNum].Add(createAnt(antX, getAntZ(lineNum, lastAnt)));
                }
                else
                {
                    float step = speed * Time.deltaTime;
                    ant.transform.position = Vector3.MoveTowards(ant.transform.position, newAntPostion(antX, antZ, ant.transform.position.y), step);
                    rotateAnt(ant);
                }
                
            }
        }

    }

    /// <summary>
    /// Creates a new instance of a Ant
    /// </summary>
    /// <param name="xPos">The x position of the new ant</param>
    /// <param name="zPos">The z position of the new ant</param>
    /// <returns>The new ant</returns>
    private GameObject createAnt(float xPos, float zPos)
    {
        GameObject ant = Instantiate(Ant);
        ant.transform.position = newAntPostion(xPos, zPos);
        rotateAnt(ant);
        return ant;
    }

    /// <summary>
    /// Sets the position of an ant and fixes the Y value
    /// </summary>
    /// <param name="xPos">The x position of the new ant</param>
    /// <param name="zPos">The z position of the new ant</param>
    /// <returns>A vector 3 position of the ant</returns>
    private Vector3 newAntPostion(float xPos, float zPos, float yPos = 0.3f)
    {

        return new Vector3(xPos, yPos, zPos);
    }


    /// <summary>
    /// Fixes the ants rotation to be upright and facing forward
    /// </summary>
    /// <param name="ant">Which ant to fix</param>
    private void rotateAnt(GameObject ant)
    {
        //ant.transform.rotation = new Quaternion(-2.674f, 0, 0, 180.0f); // Fix ants upright and facing forward
    }

    /// <summary>
    /// Gets the ants X position based on which ant line it is in
    /// </summary>
    /// <param name="lineNum">Line number of the ant</param>
    /// <returns>X position</returns>
    private float getAntX(int lineNum)
    {
        return (lineNum - numberOfAntLines/2) / 2.0f; // + Target.transform.position.x;
    }


    /// <summary>
    /// Gets the ants Y position based on the ants position in line
    /// </summary>
    /// <param name="lineNum">Ant line number</param>
    /// <param name="antNum">Ant position in line</param>
    /// <returns>Z position</returns>
    private float getAntZ(int lineNum, int antNum)
    {
        float rtnVal = -antNum * 0.75f; //Target.transform.position.z
        if (lineNum % 2 == 1)
        {
            rtnVal -= 0.75f; // Stagger effect
        }
        return rtnVal;
    }
}

