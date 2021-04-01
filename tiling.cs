using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class tiling : MonoBehaviour
{

    public int offsetX = 2; // the offset so that we dont get any weird errors

    //these are used for checking if we need to instantiate stuff
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    //used if the object is not tilable
    public bool reverseScale = false;

    // the width of our element
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    void Awake(){
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {

        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();

        //width of the element
        spriteWidth = sRenderer.sprite.bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
            // does it still need buddies, if not do nothing.
        if (hasALeftBuddy == false || hasARightBuddy == false){

            // calculate the cameras extend (half of width) of what the camerea can see in world coords
            float camHorizontalExtend = cam.orthographicSize + Screen.width/Screen.height;

            // calculate the x position where the cam can see the edge fo the sprite

            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;

            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend; 

            // checking position is bigger than or equal to the element

            //checking if we can see the edge position of the element, then calling make new buddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false){

                  MakeNewBuddy(1);
                  hasARightBuddy = true;  

            } else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false){
                MakeNewBuddy (-1);
                hasALeftBuddy = true;
            }

        }
        
    }
    // a function that creates a buddy on the side required will only take a 1 or -1
    void MakeNewBuddy(int rightOrLeft){

        //calculatin the new positon for our new buddy
        Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);


        // instantiating our new buddy and sotring him in a variable
        Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

        // gonna allow us to invert the different size of our mountains and allow them to loop
        //if not tilable lets reverse the X size of our object to get rid of ugly seams
        if (reverseScale == true){
            newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
            // checking if the tiles have buddies and if they do making sure it doesnt add one.
        newBuddy.parent = myTransform.parent;
        if(rightOrLeft > 0 ){
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        } else {
            newBuddy.GetComponent <Tiling>().hasARightBuddy = true;
        }
    }
}
