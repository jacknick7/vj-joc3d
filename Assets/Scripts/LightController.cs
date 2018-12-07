using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
    public bool day = true;

	public void setDay(bool actDay){
        day = actDay;
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(day);
        transform.GetChild(1).gameObject.SetActive(!day);
    }
}
