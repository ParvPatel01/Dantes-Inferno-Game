using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_combat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private Health enemyHealth; 
    [SerializeField] private int damage;
    
	// Sound Effects
	[SerializeField] private AudioClip attackSound;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");
        // Play attack sound
        SoundManager.instance.PlaySound(attackSound);
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);

        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
