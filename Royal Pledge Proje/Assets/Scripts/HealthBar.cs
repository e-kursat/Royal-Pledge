using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxHealth;
    public Image healthImage;

    private float healthPercent;

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
    
    public void SetHealth(float health)
    {
        healthPercent = health / maxHealth;
        healthImage.fillAmount = (healthPercent);
    }
}