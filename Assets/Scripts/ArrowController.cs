using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	void Update () {
        GameObject vehicle = GameObject.Find("Level Logic").GetComponent<LevelLogic>().getCurrentCar();
        GameObject destination = GameObject.Find("Level Logic").GetComponent<LevelLogic>().getCurrentDestination();

        Vector3 pos1 = vehicle.transform.position;
        Vector3 pos2 = destination.GetComponent<PositionRectifier>().getRectifiedPosition();

        float opo = pos2.x - pos1.x;
        float con = pos2.z - pos1.z;

        float angle = - Mathf.Atan(opo/con) * Mathf.Rad2Deg;

        if(pos1.z > pos2.z){
            if (pos1.x > pos2.x){
                angle += 180.0f;
            }
            else{
                angle -= 180.0f;
            }
        }

        //print("DATA");
        //print(opo);
        //print(con);
        //print(angle);

        transform.Rotate(new Vector3(0, 0, angle - transform.eulerAngles.z));
    }
}
