using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score s;
    private TextMeshProUGUI score;

    void Awake()
    {
        if (s != null)
            Destroy(s);
        else
            s = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    public void AddPoint(int n)
    {
        int.TryParse(score.text, out int originalScore);
        score.SetText((originalScore + n).ToString());
    }
}
