using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {
    public bool raining = false;

    public void setRaning(bool actRaining){
        raining = actRaining;
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(raining);
    }
}
