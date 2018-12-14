using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeController : MonoBehaviour {

    bool active = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) active = !active; 
        if (active) {
            if (Input.GetKeyDown(KeyCode.I)) {
                //Lights
                GameObject.Find("LightController").GetComponent<LightController>().changeDay();
            }
            else if (Input.GetKeyDown(KeyCode.O)) {
                //Night
                GameObject.Find("RainController").GetComponent<RainController>().changeRain();
            }
            else if (Input.GetKeyDown(KeyCode.P)) {
                //Fog
                GameObject.Find("FogController").GetComponent<FogController>().changeFog();
            }
            else if (Input.GetKeyDown(KeyCode.N)) {
                //NextRoute
                GameObject.Find("Level Logic").GetComponent<LevelLogic>().newRoute();
            }
        }
    }
}
