using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGuideAnt : MonoBehaviour
{
    public List<GameObject> antPositions;

    public GameObject antObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < antPositions.Count-1; i++) {
            GameObject antPos = antPositions[i];
            
            if (antPositions[i].transform.childCount == 0 && antPositions[i + 1].transform.childCount>0)
            {
                
                antPositions[i + 1].transform.GetChild(0).parent = antPositions[i].transform;

                
            }
            if(antPositions[i].transform.childCount > 0)
            {
                GameObject childAnt = antPositions[i].transform.GetChild(0).gameObject;
                childAnt.transform.position = Vector3.Slerp(childAnt.transform.position, antPos.transform.position, .1f);
                Vector3 newQuats = new Vector3(antPos.transform.rotation.eulerAngles.z, antPos.transform.rotation.eulerAngles.y - 90, antPos.transform.rotation.eulerAngles.x);

                childAnt.transform.rotation = Quaternion.Slerp(childAnt.transform.rotation, Quaternion.Euler(newQuats), .1f);
            }
            


        }

        
        if (antPositions[antPositions.Count-1].transform.childCount == 0) {
            GameObject newAnt = Instantiate(antObject);
            newAnt.transform.parent = antPositions[antPositions.Count - 1].transform;
            newAnt.transform.position = antPositions[antPositions.Count - 1].transform.position;
        }

    }

    
}
