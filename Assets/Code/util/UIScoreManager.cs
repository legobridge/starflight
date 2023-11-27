using UnityEngine;
using UnityEngine.UI;


public class UIScoreManager : MonoBehaviour
{
    public Text TimeFields;
    public float timeLimit = 60f;

    private void Update()
    {
        timeLimit -= Time.deltaTime;
        TimeFields.text = string.Format("{0}: {1:0}", "Time Remaining", timeLimit);
        if (timeLimit < 0) {
            var pc = FindObjectOfType<PlayerControl>();
            pc.OnGameOver(true);
            TimeFields.text = string.Format("{0}: {1}", "Time Remaining", 0.0f);
        }

    }
}
