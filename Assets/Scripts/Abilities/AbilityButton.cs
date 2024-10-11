using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _abilityNameText;
    [SerializeField] private TextMeshProUGUI _cooldownText;

    private Ability _ability;
    private System.Action _onClicked;

    public void Initialize(Ability ability, System.Action onClicked)
    {
        _ability = ability;
        _onClicked = onClicked;

        _abilityNameText.text = _ability.Name;
        UpdateCooldown();

        _button.onClick.AddListener(OnButtonClicked);
    }

    public void UpdateCooldown()
    {
        if (_ability.IsOnCooldown)
        {
            _cooldownText.text = $"Перезарядка: {_ability.CurrentCooldown}";
            _button.interactable = false;
        }
        else
        {
            _cooldownText.text = "";
            _button.interactable = true;
        }
    }

    private void OnButtonClicked()
    {
        _onClicked?.Invoke();
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable && !_ability.IsOnCooldown;
    }
}
