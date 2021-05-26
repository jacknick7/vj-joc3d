using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCredits : MonoBehaviour {

	public void SwitchMain() {
        GameObject.Find("MenuCredits").SetActive(false);
        transform.parent.Find("MenuMain").gameObject.SetActive(true);
    }
}
