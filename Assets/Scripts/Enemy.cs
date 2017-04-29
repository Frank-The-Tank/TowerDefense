using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public Text damageAmount;
    public Animation damageIndicator;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start ()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        damageAmount.text = amount.ToString();
        damageIndicator.Play();

        if (health <= 0 && !isDead)
        {
            Die(amount);
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die (float amount)
    {
        isDead = true;

        damageAmount.text = amount.ToString();
        damageIndicator.Play();

        PlayerStats.Money += worth;

        GameObject effect =  (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        //Destroy(transform.Find("Canvas").Find("HealthBG").gameObject);
        //Destroy(transform.Find("Canvas").gameObject);
        //Destroy(transform.Find("Canvas").gameObject);
        Destroy(gameObject);
    }

}
