using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startHealth;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = startHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Dead();
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
    }

    private void Dead()
    {
        EventBus.OnDead();
        Destroy(gameObject);
    }
}