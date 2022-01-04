using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorGenerator : MonoBehaviour
{
    [SerializeField]
    public bool isGenerating = true;
    [SerializeField]
    public ConveyorItem itemToGenerate;

    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerExit(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        Instantiate(itemToGenerate, transform.position, Quaternion.Euler(0, -90, 0));
    }
}
