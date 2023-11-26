using System;
using UnityEngine;
using UnityEngine.UI;


public class UIScoreManager : MonoBehaviour
{
    public Text TimeFields;
    public float timeLimit = 60f;
    public PlayerControl pc;

    private void Update()
    {
        timeLimit -= Time.deltaTime;
        TimeFields.text = string.Format("{0}: {1}", "Time Remaining", timeLimit);
        if (timeLimit < 0) {
            pc.OnGameOver(true);
        }

    }
}
