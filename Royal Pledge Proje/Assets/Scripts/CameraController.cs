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

    public Vector3 minValues, maxValues;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(playerPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(playerPosition.z, minValues.z, maxValues.z));
        
        transform.position = Vector3.Lerp(transform.position, boundPosition, offsetSmoothing * Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
