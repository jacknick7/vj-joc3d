using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 supposedPos;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    public void Restart(){
        supposedPos = player.transform.position + offset;
        Vector3 maxPosBL = GameObject.Find("CameraPosBL").gameObject.transform.position;
        Vector3 maxPosTR = GameObject.Find("CameraPosTR").gameObject.transform.position;

        if (supposedPos.x < maxPosBL.x) {
            transform.position = new Vector3(maxPosBL.x, transform.position.y, transform.position.z);
        }
        else if(supposedPos.x > maxPosTR.x) {
            transform.position = new Vector3(maxPosTR.x, transform.position.y, transform.position.z);

        }
        if (supposedPos.z < maxPosBL.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPosBL.z);
        }
        else if(supposedPos.z > maxPosTR.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPosTR.z);

        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        supposedPos = player.transform.position + offset;
        Vector3 maxPosBL = GameObject.Find("CameraPosBL").gameObject.transform.position;
        Vector3 maxPosTR = GameObject.Find("CameraPosTR").gameObject.transform.position;

        //print("CAMERA");
        //print(supposedPos);
        //print("MAX");
        //print(maxPosBL);
        
        if ((supposedPos.x >= maxPosBL.x) && (supposedPos.x <= maxPosTR.x)){
            transform.position = new Vector3(supposedPos.x, transform.position.y, transform.position.z);
        }
        else if (supposedPos.x < maxPosBL.x){
            transform.position = new Vector3(maxPosBL.x, transform.position.y, transform.position.z);
        }
        else if (supposedPos.x > maxPosTR.x){
            transform.position = new Vector3(maxPosTR.x, transform.position.y, transform.position.z);

        }
        if ((supposedPos.z >= maxPosBL.z) && (supposedPos.z <= maxPosTR.z)){
            transform.position = new Vector3(transform.position.x, transform.position.y, supposedPos.z);

        }
        else if (supposedPos.z < maxPosBL.z){
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPosBL.z);
        }
        else if (supposedPos.z > maxPosTR.z){
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPosTR.z);

        }
    }
}