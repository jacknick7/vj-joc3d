using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {
    public bool raining = false;

    public void setRain(bool actRaining){
        raining = actRaining;
        if (raining){
            transform.GetChild(0).gameObject.SetActive(raining);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Simulate(10.0f);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    public void changeRain(){
        raining = !raining;
        if (raining){
            transform.GetChild(0).gameObject.SetActive(raining);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Simulate(10.0f);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(raining);
    }
}
