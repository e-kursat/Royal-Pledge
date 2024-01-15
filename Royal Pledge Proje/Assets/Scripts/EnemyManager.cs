using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // enemy animator
    public Animator enemyAnimator;
    
    // enemy health
    public float maxEnemyHealth = 100;
    // enemy's current health
    public float currentEnemyHealth;

    // enemy death animation duration
    public float deathAnimationDuration = 1f;
    
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    
    // Start is called before the first frame update
    void Start()
    {
        // set enemy animator
        enemyAnimator = GetComponent<Animator>();
        
        // set first current animator
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageValue)
    {
        currentEnemyHealth -= damageValue;
        
        // play hurt animation
        enemyAnimator.SetTrigger("Hurt");
        
        if (currentEnemyHealth <= 0)
        {
            // make the enemy death
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        print("Enemy Died");
        
        // play death animation
        enemyAnimator.SetBool("isDeath", true);
        
        // disabled the enemy collider
        //GetComponent<Collider2D>().enabled = false;
        // disabled the character
        //this.enabled = false;
        
        // disabled the enemy collider
        GetComponent<Collider2D>().enabled = false;
        // disabled the character
        this.enabled = false;
        
        // deactivate all component
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }

        Destroy(gameObject, deathAnimationDuration);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
