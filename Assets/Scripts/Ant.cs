using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public MeshCollider AntCollider;

    private Rigidbody rb;
    private GameObject eatingEffectGO;
    private ParticleSystem eatingEffect;


    private bool inPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eatingEffectGO = (GameObject)Instantiate(Resources.Load("Eating"),
                                    this.transform, false);
        eatingEffect = eatingEffectGO.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Container"){
            other.transform.GetComponent<Container>().AntHit(this.gameObject);
        }
        if (other.tag == "AntGate")
        {
            inPlay = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Food") {
            Food food = collision.transform.GetComponent<Food>();
            if (food != null && !food.carried) {
                StickToFood();
                
                AntCollider.enabled = false;
                transform.SetParent(collision.transform);
                //Debug.Log("eatin food");
                eatingEffect.Play();

                food.AntHit(this.gameObject);
            }
        }
        if (collision.transform.tag == "Floor" && !inPlay)
        {
            Destroy(this.gameObject);
        }
        if (collision.transform.tag == "Floor" && inPlay)
        {
            GetComponent<AntStep>().BeginStep();
        }
        if (collision.transform.tag == "UIFood") {
            Destroy(this.gameObject);
            collision.transform.GetComponent<UIFood>().DoIt();
        }
    }

    public void StickToFood()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void OnDestroy()
    {
        Destroy(eatingEffectGO);
    }

    public void ReleaseMe() {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void DisableCollider()
    {
        SphereCollider c = GetComponentInChildren<SphereCollider>();
        c.enabled = false;
    }

    public void EnableCollider()
    {
        SphereCollider c = GetComponentInChildren<SphereCollider>();
        c.enabled = true;
    }
}
