using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilityUIManager : MonoBehaviour
{
    [SerializeField] private Transform _abilitiesParent;
    [SerializeField] private GameObject _abilityButtonPrefab;

    private List<AbilityButton> _abilityButtons = new List<AbilityButton>();

    public event Action OnAbilityUsed;

    public void Initialize(List<Ability> abilities, Action<Ability> onAbilitySelected)
    {
        if (abilities == null)
        {
            return;
        }
        if (abilities.Count == 0)
        {
            return;
        }

        foreach (var ability in abilities)
        {
            var buttonObject = Instantiate(_abilityButtonPrefab, _abilitiesParent);

            if (buttonObject == null)
            {
                continue;
            }

            var abilityButton = buttonObject.GetComponent<AbilityButton>();

            abilityButton.Initialize(ability, () =>
            {
                onAbilitySelected(ability);
                OnAbilityUsed?.Invoke();
            });
            _abilityButtons.Add(abilityButton);
        }
    }

    public void UpdateAbilityButtons()
    {
        foreach (var button in _abilityButtons)
        {
            button.UpdateCooldown();
        }
    }

    public void EnableAbilityButtons(bool enable)
    {
        foreach (var button in _abilityButtons)
        {
            button.SetInteractable(enable);
        }
    }
}
