using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
    public bool day = true;
    private float timeWantedNight = 0.0f;
    private float threshold = 0.15f;
    private float maximumTime = 15.0f;
    private int numActive;

	public void setDay(bool actDay){
        day = actDay;
        if (!day) {
            numActive = transform.GetChild(1).gameObject.transform.childCount;
        }
    }

    public void setDay(float actLuminosity){
        transform.GetChild(0).gameObject.GetComponent<Light>().intensity = actLuminosity;
        if (actLuminosity < threshold){
            if (day){
                timeWantedNight = Time.timeSinceLevelLoad;
                numActive = 0;
            }
        }
        day = (actLuminosity != 0.0f);
    }

    public void changeDay(){
        day = !day;
    }

    void FixedUpdate(){
        transform.GetChild(0).gameObject.SetActive(day);
        transform.GetChild(1).gameObject.SetActive(!day);
        if (!day && (numActive < transform.GetChild(1).gameObject.transform.childCount)) {
            if (Time.timeSinceLevelLoad - timeWantedNight > maximumTime){
                for (int i = 0; i < transform.GetChild(1).gameObject.transform.childCount; ++i){
                    transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(true);
                    ++numActive;
                }
            }
            else{
                List<int> possible = new List<int>();
                for (int i = 0; i < transform.GetChild(1).gameObject.transform.childCount; ++i){
                    if (!transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.activeSelf){
                        possible.Add(i);
                    }
                }
                if((Random.Range(0, 100) < 20) && (possible.Count>0)){
                    int pos = Random.Range(0, (possible.Count - 1));
                    transform.GetChild(1).gameObject.transform.GetChild(possible[pos]).gameObject.SetActive(true);
                    possible.RemoveAt(pos);
                    ++numActive;
                    if ((Random.Range(0, 100) < 10) && (possible.Count > 0)){
                        pos = Random.Range(0, (possible.Count - 1));
                        transform.GetChild(1).gameObject.transform.GetChild(possible[pos]).gameObject.SetActive(true);
                        possible.RemoveAt(pos);
                        ++numActive;
                        if ((Random.Range(0, 100) < 5) && (possible.Count > 0)){
                            pos = Random.Range(0, (possible.Count - 1));
                            transform.GetChild(1).gameObject.transform.GetChild(possible[pos]).gameObject.SetActive(true);
                            possible.RemoveAt(pos);
                            ++numActive;
                        }
                    }
                }
            }
        }
        if (day) {
            for (int i = 0; i < transform.GetChild(1).gameObject.transform.childCount; ++i) {
                transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            numActive = 0;
        }
    }
}
