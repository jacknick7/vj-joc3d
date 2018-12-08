using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour{
    public bool fog = false;

    public void setRaning(bool actFog){
        fog = actFog;
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(fog);
    }
}
