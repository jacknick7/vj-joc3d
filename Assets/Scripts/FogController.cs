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
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(fog);
    }
}
