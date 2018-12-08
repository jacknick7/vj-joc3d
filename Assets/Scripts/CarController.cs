using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

struct movement{
    public Vector3 position;
    public Quaternion rotation;

    public movement(Vector3 po, Quaternion ro){
        position = po;
        rotation = ro;
    }
}

public class CarController : MonoBehaviour {
    private int carStatus = 0;
    private Queue<movement> userMovement = new Queue<movement>();
    private Queue<movement> userMovementCurrent = new Queue<movement>();

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelBL;
    public WheelCollider WheelBR;

    public float topSpeed = 150f;
    public float maxTorque = 65f;
    public float maxSteerAngle = 45f;
    public float currentSpeed = 0f;
    public float maxBrakeTorque = 150f;


    private float Forward;
    private float Turn;
    private float Brake;

    public void setUserControlled(){
        carStatus = 1;
        this.gameObject.SetActive(true);
    }

    public void setUserRecorded(){
        carStatus = 2;
    }

    void Start(){
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        /*if (carStatus < 1){
            this.gameObject.SetActive(false);
        }
        else */if(carStatus < 2){
            userMovement.Clear();
        }
    }

    private void Reset(){
        currentSpeed = 0.0f;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        userMovementCurrent = new Queue<movement>(userMovement);
    }

    void FixedUpdate(){
        if (carStatus == 1){
            Forward = -Input.GetAxis("Vertical");
            Turn = Input.GetAxis("Horizontal");
            Brake = Input.GetAxis("Jump");

            WheelFL.steerAngle = maxSteerAngle * Turn;
            WheelFR.steerAngle = maxSteerAngle * Turn;

            currentSpeed = 2 * 22 / 7 * WheelBL.radius * WheelBL.rpm * 60 / 1000;

            if (currentSpeed < topSpeed)
            {
                WheelBL.motorTorque = maxTorque * Forward;
                WheelBR.motorTorque = maxTorque * Forward;
            }

            WheelBL.brakeTorque = maxBrakeTorque * Brake;
            WheelBR.brakeTorque = maxBrakeTorque * Brake;
            WheelFL.brakeTorque = maxBrakeTorque * Brake;
            WheelFR.brakeTorque = maxBrakeTorque * Brake;

            userMovement.Enqueue(new movement(transform.position, transform.rotation));
        }
        else if(carStatus == 2){
            if (userMovementCurrent.Count > 0){
                transform.position = userMovementCurrent.Peek().position;
                transform.rotation = userMovementCurrent.Peek().rotation;
                if (userMovementCurrent.Count > 1){
                    userMovementCurrent.Dequeue();
                }
            }
        }

    }
    void Update()
    {
        Quaternion flq;//rotation of wheel collider
        Vector3 flv;//position of wheel collider
        WheelFL.GetWorldPose(out flv, out flq);//get wheel collider position and rotation
        //FL.transform.position = flv;
        //FL.transform.rotation = flq;

        Quaternion Blq;//rotation of wheel collider
        Vector3 Blv;//position of wheel collider
        WheelBL.GetWorldPose(out Blv, out Blq);//get wheel collider position and rotation
        //BL.transform.position = Blv;
        //BL.transform.rotation = Blq;

        Quaternion fRq;//rotation of wheel collider
        Vector3 fRv;//position of wheel collider
        WheelFR.GetWorldPose(out fRv, out fRq);//get wheel collider position and rotation
        //FR.transform.position = fRv;
        //FR.transform.rotation = fRq;

        Quaternion BRq;//rotation of wheel collider
        Vector3 BRv;//position of wheel collider
        WheelBR.GetWorldPose(out BRv, out BRq);//get wheel collider position and rotation
                                               //BR.transform.position = BRv;
                                               //BR.transform.rotation = BRq;

        /*if ((Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.125f) && (carStatus == 1)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/

    }

    public void setCarStatus(int status) {
        carStatus = status;
        gameObject.SetActive(!(status == 0));
    }

    public void setCarStatusAndReset(int status) {
        carStatus = status;
        gameObject.SetActive(!(status == 0));
        Reset();
        //print("RESETED WITH POSITIONS");
        //print(originalPosition);
    }
}