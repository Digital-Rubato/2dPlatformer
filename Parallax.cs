using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;     // array list of all the back and foregrounds to be parallaxed.
    private float[] parallaxScales; //proportion of the camera's movement to move the backgrounds by
    public float smoothing =1f; // How smooth the parallax is going to be. Make sure to set this above 0 or the parallax effect will nto work

    private Transform cam; // ref to the main cameras transform good to store for later

    private Vector3 previousCamPos; // stores the position of the camera in the previous frame.

    void Awake() {
        // awake is called before start(). Will call all the logic before the start function but after the game objs are set up(like setting up variables or references between scripts)

        // set up the camera reference
        cam = Camera.main.transform;
    }
    
    // Start is called before the first frame update (used for initialization)
    void Start()
    {
        // the previous frame had the current frame's camera position
        previousCamPos = cam.position;
        //assigning BG elements a parallax scale

        parallaxScales = new float[backgrounds.Length];
        //assigning coresponding parallaxScales
        for(int i = 0; i <backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //for each background 
        for (int i =0; i< backgrounds.Length; i++){
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position whcih is the background's current position with it's target x position

            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //this could be done with the Y value as well but as of now we are not moving the camera up and down too much. ADD THIS LATER.

            //lerp or fade between current pos and the target pos

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            //time.deltatime converts frames to seconds
        }

        // set the previous CamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
