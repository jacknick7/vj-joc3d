using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongWayController : MonoBehaviour {

    private float timeActivated = -1.0f;

    public void activateWrongWay() {
        var tmpColor = transform.GetComponent<Image>().color;
        tmpColor.a = 1.0f;
        transform.GetComponent<Image>().color = tmpColor;
        timeActivated = Time.timeSinceLevelLoad;
    }

    void Update(){
        if ((timeActivated > 0.0f) && (Time.timeSinceLevelLoad > (timeActivated + 1.0f))){
            var tmpColor = transform.GetComponent<Image>().color;
            tmpColor.a = 0.0f;
            transform.GetComponent<Image>().color = tmpColor;
            timeActivated = -1.0f;
        }
    }
}
