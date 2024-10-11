using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _healthText;

    public void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
        UpdateHealthText(maxHealth);
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
        UpdateHealthText(health);
    }

    private void UpdateHealthText(int health)
    {
        _healthText.text = $"{health}/{_slider.maxValue}";
    }
}
