using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    [HideInInspector]
    public int carryNum=0;
    public int maxCarry = 5;
    public float IndicatorOffset = 2f;
    private Color minGlowColor;
    private Color glowColor;
    private Color maxGlowColor;
    private List<GameObject> antList = new List<GameObject>();

    private GameObject ringIndicator;
    private TextMeshPro remainAntTMP;
    private Image ring;
    private bool triggeredScoreboard = false;

    // Start is called before the first frame update
    void Start()
    {
        minGlowColor = new Color(255.0f, 0, 0);
        maxGlowColor = new Color(0, 255.0f, 0);
        glowColor = minGlowColor;

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(-180, 0, -90);

        ringIndicator = (GameObject)Instantiate(Resources.Load("RingIndicator"), this.transform.position, rot);

        remainAntTMP = ringIndicator.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshPro>();
        ring = ringIndicator.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        ring.fillAmount = 0f;
        remainAntTMP.text = (maxCarry - carryNum).ToString();

        StartCoroutine("FirstTimeBlinkIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        ringIndicator.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + IndicatorOffset, this.transform.position.z);
        ringIndicator.transform.LookAt(Camera.main.transform, Vector3.right);

        //if you hit max capacity for ants
        if (carryNum >= maxCarry) {
            if (!triggeredScoreboard)
            {
                Scoreboard.GainFood(gameObject.name);
                triggeredScoreboard = true;
            }


            for (int i = 0; i < antList.Count; i++)
            {
                Destroy(antList[i].gameObject);
            }

            Destroy(transform.parent.gameObject);

        }
    }

    //call this in the ant script when you land an ant
    public void AntHit(GameObject ant) {
        carryNum += 1;
        StartCoroutine("IncrementRing");
        int num = ((maxCarry - carryNum) < 0) ? 0 : maxCarry - carryNum;
        remainAntTMP.text = num.ToString();
        StartCoroutine("BlinkIndicator");

        antList.Add(ant);
        glowColor = getGlowColor();
        // GetComponentInParent<Light>().color = glowColor;
    }

    private Color getGlowColor()
    {

        float percentComplete = 1.0f * carryNum / maxCarry;
        int colorInterp = Mathf.RoundToInt(percentComplete * 255 * 2); // min is 255, 0, 0. middle is 255, 255, 0. max is 0, 255, 0;
        if (colorInterp <= 255)
        {
            return new Color(1.0f, 1.0f * colorInterp / 255, 0);
        }
        else
        {
            int invert = colorInterp - 255;
            return new Color(1.0f * (255 - invert) / 255, 1.0f, 0);
        }
    }

    IEnumerator IncrementRing()
    {
        float fillAmount = (float)carryNum / maxCarry;

        //particleRing.Play();
        Debug.Log("fill by " + fillAmount);
        while (ring.fillAmount < fillAmount)
        {
            if (ring.gameObject.activeSelf)
            {
                ring.fillAmount += Time.deltaTime;
                yield return new WaitForSeconds(0);
            }
            else break;
        }
        if (ring.gameObject.activeSelf)
            ring.fillAmount = fillAmount;
    }

    private void OnDestroy()
    {
        Destroy(ringIndicator);
    }

    IEnumerator FirstTimeBlinkIndicator()
    {
        float timeTotal = 8f;
        float time = timeTotal;
        float threshold = 4.5f;
        ring.fillAmount = 1;
        while (time > threshold)
        {
            float a = Mathf.PingPong(Time.time, 0.8f) + 0.2f;
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, a);
            remainAntTMP.text = ((int)((1 - (time - threshold) / (timeTotal - threshold)) * (maxCarry - carryNum + 1))).ToString();
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, a);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0f);
            ring.fillAmount = (time - threshold) / (timeTotal - threshold);
        }
        while (time > 0)
        {
            float a = Mathf.PingPong(Time.time, 0.8f) + 0.2f;
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, a);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, a);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        ring.fillAmount = 0;
        StartCoroutine("FadeOutAlpha");
    }

    IEnumerator BlinkIndicator()
    {
        float time = 6f;
        while (time > 0)
        {
            float a = Mathf.PingPong(Time.time, 0.8f) + 0.2f;
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, a);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, a);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        StartCoroutine("FadeOutAlpha");
    }

    IEnumerator FadeOutAlpha()
    {
        while (ring.color.a > 0)
        {
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, ring.color.a - Time.deltaTime);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, remainAntTMP.color.a - Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
        ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, 0f);
        remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, 0f);
    }
}
