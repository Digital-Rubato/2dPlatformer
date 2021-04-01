using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
 
    public int rotationOffset = 90;
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position; // subtracting the position of the player from the moues position
        difference.Normalize (); //x+y+z = 1 Normalize means that we keep the same proportions but making them smaller so when we add them it equals one (normalizing the vector meaning that all the sum of the vector will be equal to 1)

        float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees

        transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);

        
    }
}
