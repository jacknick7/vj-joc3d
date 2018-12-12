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
            GameObject.Find("AudioRain").GetComponent<AudioSource>().Play();
        }
        else GameObject.Find("AudioRain").GetComponent<AudioSource>().Stop();
    }

    public void changeRain(){
        raining = !raining;
        if (raining){
            transform.GetChild(0).gameObject.SetActive(raining);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Simulate(10.0f);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            GameObject.Find("AudioRain").GetComponent<AudioSource>().Play();
        }
        else GameObject.Find("AudioRain").GetComponent<AudioSource>().Stop();
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(raining);
    }
}
