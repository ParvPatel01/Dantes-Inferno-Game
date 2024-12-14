using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    public DeathUIScript deathScreen;
    

    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                if (gameObject.CompareTag("Player")){
                    SoundManager.instance.PlaySound(deathSound);
                    deathScreen.ShowDeathScreen();
                    deathScreen.SetText("You died!");
                    gameObject.GetComponent<PlayerMovement>().enabled = false;
                    gameObject.GetComponent<Player_combat>().enabled = false;
                }
                else
                    SoundManager.instance.PlaySound(hurtSound);

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                //Enemy is dead and destroy the game object
                if (gameObject.CompareTag("Enemy")) {

                    // disable box collider
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    
                    gameObject.GetComponentInParent<EnemyPatrol>().speed = 0;
                }

                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}