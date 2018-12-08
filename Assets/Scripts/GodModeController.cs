using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeController : MonoBehaviour {
    void Update(){
        if (Input.GetKeyDown(KeyCode.Z)){
            //Lights
            GameObject.Find("LightController").GetComponent<LightController>().changeDay();
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            //Night
            GameObject.Find("RainController").GetComponent<RainController>().changeRain();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            //Fog
            GameObject.Find("FogController").GetComponent<FogController>().changeFog();
        }
        else if (Input.GetKeyDown(KeyCode.N)){
            //NextRoute
            GameObject.Find("Level Logic").GetComponent<LevelLogic>().newRoute();
        }
    }
}
