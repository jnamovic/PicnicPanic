using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private static Scoreboard _instance;

    public static Scoreboard Instance { get { return _instance; } }
    // private static int numChip = 0;
    // private static int numDonut = 0;
    // private static int numStrawberry = 0;
    // private static int numBlueberry = 0;
    // private static int numHoney = 0;
    // private static int numJello = 0;
    // private static int numChicken = 0;
    private static Dictionary<string, int> itemDict = new Dictionary<string, int>();
    private static List<GameObject> scoreboardItems = new List<GameObject>();
    private static Transform DisplayTransform;
    private static CanvasGroup cg;
    private static Animator animator;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        DictInitialize();
        DisplayTransform = GameObject.Find("ScoreboardDisplay").transform;
        animator = gameObject.GetComponent<Animator>();
        cg = gameObject.transform.GetChild(0).GetComponent<CanvasGroup>();
        cg.alpha = 0;

        Collapse();
    }

    public static void GainFood(string foodtype)
    {
        foodtype = foodtype.ToLower();
        if(foodtype.Contains("donut"))
        {
            itemDict["Donut"] += 1;

        } else if (foodtype.Contains("chip"))
        {
            itemDict["Chip"] += 1;
        }
        else if (foodtype.Contains("strawberry"))
        {
            itemDict["Strawberry"] += 1;
        }
        else if (foodtype.Contains("blueberry"))
        {
            itemDict["Blueberry"] += 1;
        }
        else if (foodtype.Contains("honey"))
        {
            itemDict["Honey"] += 1;
        }
        else if (foodtype.Contains("jello"))
        {
            itemDict["Jello"] += 1;
        }
        else if (foodtype.Contains("chicken"))
        {
            itemDict["Chicken"] += 1;
        }
        Debug.Log("scored " + foodtype);
    }

    private static void Display()
    {
        foreach(GameObject existingItem in scoreboardItems)
        {
            Destroy(existingItem);
        }
        int count = 1;

        foreach (KeyValuePair<string, int> p in itemDict)
        {
            if (p.Value > 0) count += 1;
        }
        Debug.Log("counted " + count);
        if (count > 1)
        {
            float interval = 1.5f / count;
            int i = 0;
            float pos;
            foreach (KeyValuePair<string, int> p in itemDict)
            {
                Debug.Log(p.Value);
                if (p.Value > 0)
                {
                    // instantiate item
                    GameObject item = (GameObject)Instantiate(Resources.Load("ScoreboardItem"), DisplayTransform);
                    scoreboardItems.Add(item);
                    pos = 0.5f - i * interval;
                    item.GetComponent<ScoreboardItem>().LoadContent(p.Key, p.Value, pos);
                    i += 1;
                }
            }
        }
    }

    private static void DestroyItems()
    {
        foreach (GameObject existingItem in scoreboardItems)
        {
            Destroy(existingItem);
        }
    }

    private void DictInitialize()
    {
        itemDict.Add("Chip", 0);
        itemDict.Add("Donut", 0);
        itemDict.Add("Blueberry", 0);
        itemDict.Add("Strawberry", 0);
        itemDict.Add("Jello", 0);
        itemDict.Add("Honey", 0);
        itemDict.Add("Chicken", 0);
    }

    public IEnumerator FadeInCG()
    {
        while(cg.alpha < 1)
        {
            cg.alpha += 5 * Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        cg.alpha = 1;
        Display();
    }

    public IEnumerator FadeOutCG()
    {
        while (cg.alpha > 0)
        {
            cg.alpha -= 6 * Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        cg.alpha = 0;
        DestroyItems();
    }

    public static void Popup()
    {
        if(animator != null)
        {
            animator.SetBool("Enter", true);
        }
    }

    public static void Collapse()
    {
        if (animator != null)
        {
            animator.SetBool("Enter", false);
        }
    }

    public static int GetScore()
    {
        int score = 0;
        foreach (KeyValuePair<string, int> p in itemDict)
        {
            score += p.Value;
        }
        return score;
    }
}
