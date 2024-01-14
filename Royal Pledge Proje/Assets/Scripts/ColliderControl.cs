using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    public BoxCollider2D standCollider;
    public BoxCollider2D crouchCollider;
    public CircleCollider2D circleCollider;
    
    private PlayerScript playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();

        standCollider.enabled = true;
        crouchCollider.enabled = false;
        circleCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is in the air
        if (playerScript.isGround == false)
        {
            standCollider.enabled = true;
            crouchCollider.enabled = false;
            circleCollider.enabled = true;
        }
        else
        {
            // if player is crouching
            if (playerScript.isCrouching)
            {
                standCollider.enabled = false;
                crouchCollider.enabled = true;
                circleCollider.enabled = true;
            }
            
            // if player is standing
            else
            {
                standCollider.enabled = true;
                crouchCollider.enabled = false;
                circleCollider.enabled = true;
            }
        }
    }
}
