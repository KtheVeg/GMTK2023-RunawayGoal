using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    GameFlow gameFlow;
    [SerializeField]
    TMPro.TextMeshProUGUI timerText;
    [SerializeField]
    UnityEngine.UI.Image timerBar;
    
    void Update()
    {
        timerBar.GetComponent<RectTransform>().localScale = new Vector3(gameFlow.isRoaming ? gameFlow.timer / gameFlow.roamingPhaseDuration : gameFlow.timer / gameFlow.stationaryPhaseDuration, 1, 1);
        timerBar.color = gameFlow.isRoaming ? Color.green : Color.red;
        timerText.text = Mathf.RoundToInt(gameFlow.timer).ToString();
    }
}
