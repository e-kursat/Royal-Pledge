using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AudioSource hurtSoundEffect;
    // player animator
    public Animator playerAnimator;
    
    // player health
    public float maxPlayerHealth = 100;
    // player's current health
    public float currentPlayerHealth;

    // player death animation duration
    public float deathAnimationDuration = 1f;
    
    public HealthBar healthBar;
    
    public GameObject popUpDamage;
    private TextMeshPro popUpText;
    
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    
    private void Awake()
    {
        // set player animator
        playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpText = popUpDamage.transform.GetChild(0).GetComponent<TextMeshPro>();
        // set max health for health bar
        healthBar.SetMaxHealth(maxPlayerHealth);
        
        // set first current health
        currentPlayerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentPlayerHealth += 20;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentPlayerHealth -= 20;
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentPlayerHealth = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentPlayerHealth = 100;
        }
        
        healthBar.SetHealth(currentPlayerHealth);
    }

    private float totalDamage = 0;
        
    public void TakeDamage(float damageValue)
    {
        currentPlayerHealth -= damageValue;
        
        healthBar.SetHealth(currentPlayerHealth);
        
        if (damageValue > 10)
            hurtSoundEffect.Play();
        {
            popUpText.SetText(damageValue.ToString());
            Instantiate(popUpDamage, transform.position, Quaternion.identity);
        }
        else
        {
            if (totalDamage >= 10)
            {
                popUpText.SetText(10.ToString());
                Instantiate(popUpDamage, transform.position, Quaternion.identity);
                
                totalDamage = 0;
            }
            else
            {
                totalDamage += damageValue;
            }
        }
        
        // play hurt animation
        playerAnimator.SetTrigger("Hurt");
        
        if (currentPlayerHealth <= 0)
        {
            // make the player death
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        print("player Died");
        
        // play death animation
        playerAnimator.SetBool("isDeath", true);
        
        // disabled the player collider
        //GetComponent<Collider2D>().enabled = false;
        // disabled the character
        //this.enabled = false;
        
        // deactivate all component
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }

        //Destroy(gameObject, deathAnimationDuration);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}