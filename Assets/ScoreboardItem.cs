using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardItem : MonoBehaviour
{
    private GameObject model;
    private TextMeshProUGUI score;
    private bool isLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isLoaded)
        {
            model.transform.localRotation = Quaternion.Euler(0, (Time.time * 15000) / 180, 90);
        }
    }

    public void LoadContent(string modelname, int num, float ypos)
    {
        isLoaded = true;
        Debug.Log("load content " + modelname + " num = " + num.ToString());
        model = transform.GetChild(0).gameObject;
        score = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        this.transform.localPosition = new Vector3(0, ypos, 0);
        GameObject modelPrefab = (GameObject)Instantiate(Resources.Load(modelname), model.transform);
        if(modelname.StartsWith("Straw", System.StringComparison.Ordinal) || modelname.StartsWith("Blue", System.StringComparison.Ordinal))
        {
            model.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if(modelname.StartsWith("Jell", System.StringComparison.Ordinal)){
            model.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        else if(modelname.StartsWith("Chick", System.StringComparison.Ordinal))
        {
            model.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        Destroy(modelPrefab.GetComponent<Rigidbody>());
        Destroy(modelPrefab.GetComponent<DonutWiggle>());
        modelPrefab.transform.localPosition = Vector3.zero;
        modelPrefab.transform.localRotation = Quaternion.Euler(Vector3.zero);
        modelPrefab.transform.localScale = Vector3.one;
        Destroy(modelPrefab.GetComponent<Food>());
        score.text = "x " + num.ToString();
    }
}
