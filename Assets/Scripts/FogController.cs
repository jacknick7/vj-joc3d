using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour{
    public bool fog = false;

    public void setFog(bool actFog){
        fog = actFog;
    }

    public void changeFog(){
        fog = !fog;
        if (fog){
            transform.GetChild(0).gameObject.SetActive(fog);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Simulate(120.0f);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(fog);
    }
}
