using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour {
	void Update(){
        float time = GameObject.Find("Level Logic").GetComponent<LevelLogic>().getTime();
        int minutes = Mathf.RoundToInt(Mathf.Floor(time/60.0f));
        int seconds = Mathf.RoundToInt(time-minutes*60);
        string timeShow = minutes + ":";
        if(seconds >= 60){
            timeShow = (minutes + 1) + ":00";
        }
        else if(seconds < 10){
            timeShow += "0" + seconds;
        }
        else{
            timeShow += seconds;
        }
        GetComponent<TextMeshProUGUI>().SetText(timeShow);
    }
}
