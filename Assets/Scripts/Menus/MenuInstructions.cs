using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInstructions : MonoBehaviour {

    public void SwitchMain() {
        GameObject.Find("MenuInstructions").SetActive(false);
        transform.parent.Find("MenuMain").gameObject.SetActive(true);
    }
}
