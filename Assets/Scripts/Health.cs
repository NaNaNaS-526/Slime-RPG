using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int moneyForEnemy;
    [SerializeField] private float restoreHealthTime;
    [SerializeField] private int restoreHealthAmount;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = startHealth;
        healthText.text = _currentHealth.ToString();
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Dead();
        UpdateUI();
    }
    

    private void Dead()
    {
        EventBus.OnDead();
        EventBus.OnGotMoney(moneyForEnemy);
        if (gameObject.CompareTag("Player")) SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        healthBar.fillAmount = _currentHealth / startHealth;
        healthBar.color = gradient.Evaluate(_currentHealth / startHealth);
        healthText.text = _currentHealth.ToString();
        healthText.color = gradient.Evaluate(_currentHealth / startHealth);
    }
}