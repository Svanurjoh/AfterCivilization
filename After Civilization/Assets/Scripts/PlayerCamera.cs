using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
	public Transform target;                            // Target to follow
    public float targetHeight = 1.7f;                       // Vertical offset adjustment
    public float distance = 12.0f;                          // Default Distance
    public float offsetFromWall = 0.1f;                     // Bring camera away from any colliding objects
    public float maxDistance = 20;                      // Maximum zoom Distance
    public float minDistance = 0.6f;                        // Minimum zoom Distance
    public float xSpeed = 200.0f;                           // Orbit speed (Left/Right)
    public float ySpeed = 200.0f;                           // Orbit speed (Up/Down)
    public float yMinLimit = -80;                           // Looking up limit
    public float yMaxLimit = 80;                            // Looking down limit
    public float zoomRate = 40;                             // Zoom Speed
    public float rotationDampening = 3.0f;              // Auto Rotation speed (higher = faster)
    public float zoomDampening = 5.0f;                  // Auto Zoom speed (Higher = faster)
    LayerMask collisionLayers = -1;     // What the camera will collide with
 
    public bool lockToRearOfTarget;            
    public bool allowMouseInputX = true;               
    public bool allowMouseInputY = true;               
   
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    public float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind;
   
    public GameObject userModel;
    public bool inFirstPerson;
       
    void Start () {
 
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
       
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            //rigidbody.freezeRotation = true;
       
        if (lockToRearOfTarget)
            rotateBehind = true;
    }
   
    void Update () {
       
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
           
            if(inFirstPerson == true) {
               
                minDistance = 7;
                desiredDistance = 7.1f;
                userModel.SetActive(true);
                inFirstPerson = false;
            }
        }
       
        /*if(desiredDistance < 7) {
           
            minDistance = 0;
            desiredDistance = 0;
            userModel.SetActive(false);
            inFirstPerson = true;
        }  */
    }
   
    //Only Move camera after everything else has been updated
    void LateUpdate ()
    {
        Vector3 vTargetOffset3;
       
        // Set camera rotation
        //Quaternion rotation = Quaternion.Euler (yDeg, xDeg, 0);
       
        // Calculate the desired distance
        desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (desiredDistance);
        desiredDistance = Mathf.Clamp (desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;
       
        /*// Calculate desired camera position
        Vector3 vTargetOffset = new Vector3 (0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);
       
        // Check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3 (target.position.x, target.position.y + targetHeight, target.position.z);
       
        // If there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if (Physics.Linecast (trueTargetPosition, position,out collisionHit, collisionLayers))
        {
            correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - offsetFromWall;
            isCorrected = true;
        }
       
        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;
       
        // Keep within limits
        currentDistance = Mathf.Clamp (currentDistance, minDistance, maxDistance);
       
        // Recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);*/
       
        //Finally Set rotation and position of camera
		transform.position = target.TransformPoint (new Vector3(0, 4, -desiredDistance));
		transform.LookAt (target);
    }
 
    void ClampAngle (float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
 
        yDeg = Mathf.Clamp(angle, -60, 80);
    }
}
