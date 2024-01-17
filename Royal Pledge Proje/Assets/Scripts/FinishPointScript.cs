using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.NextLevel();
            this.enabled = false;
        }
    }
}
