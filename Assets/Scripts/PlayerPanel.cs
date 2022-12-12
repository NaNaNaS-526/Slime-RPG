using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private Button attackDamageButton;
    [SerializeField] private Button attackSpeedButton;


    private int _attackDamageEnhancementCost = 10;
    private int _attackSpeedEnhancementCost = 10;

    private void OnEnable()
    {
        attackDamageButton.onClick.AddListener(EnhanceAttackDamage);
        attackSpeedButton.onClick.AddListener(EnhanceAttackSpeed);
        EventBus.OnMoneyUpdated += MoneyUpdate;
        EventBus.OnAttackDamageUpdated += UpdateAttackDamageText;
        EventBus.OnAttackSpeedUpdated += UpdateAttackSpeedText;
    }

    private void OnDisable()
    {
        EventBus.OnMoneyUpdated -= MoneyUpdate;
        EventBus.OnAttackDamageUpdated -= UpdateAttackDamageText;
        EventBus.OnAttackSpeedUpdated -= UpdateAttackSpeedText;
    }

    private void MoneyUpdate(float currentMoney)
    {
        moneyText.text = currentMoney.ToString();
    }

    private void EnhanceAttackDamage()
    {
        if (int.Parse(moneyText.text) >= _attackDamageEnhancementCost)
        {
            EventBus.OnAttackDamageEnhanced();
            EventBus.OnSpentMoney(_attackDamageEnhancementCost);
            _attackDamageEnhancementCost += 5;
            attackDamageButton.GetComponentInChildren<TextMeshProUGUI>().text = _attackDamageEnhancementCost.ToString();
        }
    }

    private void EnhanceAttackSpeed()
    {
        if (int.Parse(moneyText.text) >= _attackSpeedEnhancementCost && _attackSpeedEnhancementCost < 25)
        {
            EventBus.OnAttackSpeedEnhanced();
            EventBus.OnSpentMoney(_attackSpeedEnhancementCost);
            _attackSpeedEnhancementCost += 5;
            attackSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = _attackSpeedEnhancementCost.ToString();
        }
    }

    private void UpdateAttackDamageText(int damage)
    {
        attackDamageText.text = $"Atck: {damage}";
    }

    private void UpdateAttackSpeedText(float speed)
    {
        attackSpeedText.text = $"Aspd: {speed}";
        if (speed <= 0.5f) attackSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Max";
    }
}