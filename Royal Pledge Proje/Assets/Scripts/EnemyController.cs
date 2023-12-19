using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator enemyAnimator;
    
    // enemy health
    public int maxEnemyHealth = 100;
    
    // enemy's current health
    private int currentEnemyHealth;

    // enemy death animation duration
    public float deathAnimationDuration = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageValue)
    {
        currentEnemyHealth -= damageValue;
        
        enemyAnimator.SetTrigger("Hurt");
        
        if (currentEnemyHealth <= 0)
        {
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        print("Enemy Died");
        enemyAnimator.SetBool("isDeath", true);
        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
        Destroy(gameObject, deathAnimationDuration);

    }
}
