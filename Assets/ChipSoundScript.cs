using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSoundScript : MonoBehaviour
{
    private bool hasFallen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Floor") && !hasFallen)
        {
            hasFallen = true;
            FMODManager.Instance.ChipSound();
        }
    }
}
