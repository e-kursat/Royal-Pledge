using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player object
    public GameObject player;
    
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        
        // playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        
        // player looks right
        if (player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        // player looks left
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
