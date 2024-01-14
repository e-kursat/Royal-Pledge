using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // player animator
    public Animator playerAnimator;
    
    // player health
    public float maxPlayerHealth = 100;
    // player's current health
    public float currentPlayerHealth;

    // player death animation duration
    public float deathAnimationDuration = 1f;
    
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    
    // Start is called before the first frame update
    void Start()
    {
        // set player animator
        playerAnimator = GetComponent<Animator>();
        
        // set first current animator
        currentPlayerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageValue)
    {
        currentPlayerHealth -= damageValue;
        
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