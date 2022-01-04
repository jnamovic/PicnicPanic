using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStep : MonoBehaviour
{
    public float step=5;
    public float rotSpeed;
    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Random.Range(1, 20) < 2)
            {
                ChangeDir();
            }


            transform.GetComponent<Rigidbody>().velocity = transform.forward;
        }
    }
    void ChangeDir() {
        Quaternion newRot = Quaternion.Euler(transform.rotation.x,transform.rotation.y + Random.Range(-180, 180), transform.rotation.z);

        //transform.rotation = newRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotSpeed);
    }

    public void BeginStep() {
        transform.GetComponent<Rigidbody>().isKinematic=true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        moving = true;
        //transform.position = new Vector3(transform.position.x, .33f, transform.position.y);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(DestructionTimer());

    }

    private IEnumerator DestructionTimer()
    {

       yield return new WaitForSeconds(5);
        Destroy(this.gameObject);

    }

}
