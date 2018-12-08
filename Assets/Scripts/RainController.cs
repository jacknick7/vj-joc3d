using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {
    public bool raining = false;

    public void setRanin(bool actRaining){
        raining = actRaining;
    }

    public void changeRain(){
        raining = !raining;
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(raining);
    }
}
