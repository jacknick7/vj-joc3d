using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadVariables : MonoBehaviour {

    public bool win = true;
    
	void Start () {
        if (win){
            float time = GlobalVars.Time;
            int minutes = Mathf.RoundToInt(Mathf.Floor(time / 60.0f));
            int seconds = Mathf.RoundToInt(time - minutes * 60);
            string timeShow = minutes + ":";
            if (seconds >= 60){
                timeShow = (minutes + 1) + ":00";
            }
            else if (seconds < 10){
                timeShow += "0" + seconds;
            }
            else{
                timeShow += seconds;
            }
            GameObject.Find("TextTime").GetComponent<TextMeshProUGUI>().SetText(timeShow);
        }
        else{
            GameObject.Find("TextLevel").GetComponent<TextMeshProUGUI>().SetText(GlobalVars.Level + "");
            GameObject.Find("TextRoute").GetComponent<TextMeshProUGUI>().SetText(GlobalVars.Route + "");
        }
    }
}
