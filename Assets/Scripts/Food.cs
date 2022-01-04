using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class Food : MonoBehaviour
{
    private Color minGlowColor;
    private Color glowColor;
    private Color maxGlowColor;
    public int maxCarry = 5;
    [SerializeField]
    private bool destroyOnMaxCarry = false;
    public GameObject overrideTargetObject = null;
    public UnityEvent onMaxCarry;
    public float IndicatorOffset = 1.2f;

    [HideInInspector]
    public int carryNum = 0;
    private List<GameObject> antList = new List<GameObject>();
    [HideInInspector]
    public bool carried = false;

    private GameObject ringIndicator;
    private TextMeshPro remainAntTMP;
    private Image ring;
    private bool triggeredScoreboard = false;
    [SerializeField]
    private bool indicatorShouldFadeOut = true;
    private bool maxCarryInvoked = false;

    public bool scoreOnHit = false;

    // Start is called before the first frame update
    void Start()
    {
        if (overrideTargetObject == null) {
            overrideTargetObject = this.gameObject;
        }
        minGlowColor = new Color(255.0f, 0, 0);
        maxGlowColor = new Color(0, 255.0f, 0);

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(-180, 0, -90);

        ringIndicator = (GameObject)Instantiate(Resources.Load("RingIndicator"), this.transform.position, rot);

        remainAntTMP = ringIndicator.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshPro>();
        ring = ringIndicator.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        ring.fillAmount = 0f;
        remainAntTMP.text = (maxCarry - carryNum).ToString();

        StartCoroutine("FirstTimeBlinkIndicator");

        //Debug.Log("Food initialized " + gameObject.name);
        if(gameObject.name.Contains("Tutorial")) {
            indicatorShouldFadeOut = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ringIndicator != null) {
            float zpos = indicatorShouldFadeOut ? this.transform.position.z : this.transform.position.z - 1;
            ringIndicator.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + IndicatorOffset, zpos);
            ringIndicator.transform.LookAt(Camera.main.transform, Vector3.right);
        }
        
        //Quaternion rot = ringIndicator.transform.rotation;
        //rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, -90);
        //ringIndicator.transform.rotation = rot;

        //if you hit max capacity for ants
        if (carryNum >= maxCarry && !carried)
        {
            carried = true;

            if (!triggeredScoreboard)
            {
                Scoreboard.GainFood(gameObject.name);
                triggeredScoreboard = true;
            }

            for (int i = 0; i < antList.Count; i++) {
                Destroy(antList[i].gameObject);
            }
            if (GetComponent<DonutWiggle>() != null)
            {
                GetComponent<DonutWiggle>().StopWiggle();
            }
            var moveTo = overrideTargetObject.GetComponent<MoveTo>();
            var navAgent = overrideTargetObject.GetComponent<NavMeshAgent>();
            var rigidbody = overrideTargetObject.GetComponent<Rigidbody>();
            if (moveTo != null && navAgent != null && rigidbody != null) {
                navAgent.enabled = true;
                moveTo.enabled = true;
                rigidbody.isKinematic = true;

                var navObstacle = overrideTargetObject.GetComponent<NavMeshObstacle>();
                if(navObstacle != null) {
                    navObstacle.enabled = false;
                }
            }

            if (destroyOnMaxCarry) {
                Destroy(overrideTargetObject);
            }

            if(!maxCarryInvoked)
            {
                onMaxCarry.Invoke();
                maxCarryInvoked = true;
            }

        }
    }

    //call this in the ant script when you land an ant
    public void AntHit(GameObject ant)
    {
        Debug.Log("Ant hit");

        carryNum += 1;
        FMODManager.Instance.ScoreSound();

        StartCoroutine("IncrementRing");
        int num = ((maxCarry - carryNum) < 0) ? 0 : maxCarry - carryNum;
        remainAntTMP.text = num.ToString();
        
        StartCoroutine("BlinkIndicator");

        if(scoreOnHit) {
            Scoreboard.GainFood(gameObject.name);
        }

        antList.Add(ant);
        glowColor = getGlowColor();
        //GetComponent<Light>().color = glowColor;
        if (GetComponent<DonutWiggle>() != null) {
            GetComponent<DonutWiggle>().SetWiggle(carryNum, maxCarry);
        }
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
        while(ring.fillAmount < fillAmount) {
            ring.fillAmount += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
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
        Debug.Log("First time blinking " + gameObject.name);
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
        if (indicatorShouldFadeOut || carryNum == maxCarry)
        {
            StartCoroutine("FadeOutAlpha");
        }
        else
        {
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, 1f);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, 1f);
        }
    }

    IEnumerator BlinkIndicator() {
        float time = 6f;
        if (carryNum == maxCarry && !indicatorShouldFadeOut) time = 0f;
        Debug.Log("Blinking " + time + " s " + gameObject.name );
        while (time > 0)
        {
            float a = Mathf.PingPong(Time.time, 0.8f) + 0.2f;
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, a);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, a);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        if(indicatorShouldFadeOut || carryNum == maxCarry)
        {
            StartCoroutine("FadeOutAlpha");
        }
        else
        {
            ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, 1f);
            remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, 1f);
        }
    }

    IEnumerator FadeOutAlpha()
    {
        if(indicatorShouldFadeOut)
        {
            Debug.Log("Fade out alpha");
            while (ring.color.a > 0)
            {
                ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, ring.color.a - Time.deltaTime);
                remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, remainAntTMP.color.a - Time.deltaTime);
                yield return new WaitForSeconds(0f);
            }
        }

        ring.color = new Color(ring.color.r, ring.color.g, ring.color.b, 0f);
        remainAntTMP.color = new Color(remainAntTMP.color.r, remainAntTMP.color.g, remainAntTMP.color.b, 0f);
        //if (maxCarry == carryNum) Destroy(ringIndicator);
    }

    public void DestroySelf()
    {
        Destroy(this);
    }
}
