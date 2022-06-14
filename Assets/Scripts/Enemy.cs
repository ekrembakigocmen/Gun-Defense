using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed = 10f;
    public float startHealth = 100;
    private float health;
    public int gainMoney = 50;
    private bool isDead = false;
    public bool isFirstSpawn = false;
    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {

        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {

        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {

            Die();

        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    void Die()
    {
        isDead = true;
        PlayerStats.Money += gainMoney;
        Destroy(gameObject);
        WaveSpawner.IsEnemies--;
        WaveSpawner.EnemiesAlive--;

    }



}
