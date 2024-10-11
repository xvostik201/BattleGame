using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EnemyUnit : Unit
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _effectIconsParent; 
    [SerializeField] private Sprite _barrierSprite;
    [SerializeField] private Sprite _regenerationSprite;
    [SerializeField] private Sprite _burningSprite;

    private Dictionary<EffectType, Image> _activeEffectIcons = new Dictionary<EffectType, Image>();
    private List<Ability> _abilities;
    private System.Random _random = new System.Random();

    protected override void Awake()
    {
        base.Awake();
        InitializeAbilities();
    }

    protected override void UpdateHealthUI()
    {
        _healthBar.SetHealth(CurrentHealth);
    }

    protected override void UpdateEffectIcons()
    {
        var effectsToRemove = new List<EffectType>();
        foreach (var effectType in new List<EffectType>(_activeEffectIcons.Keys))
        {
            if (!_activeEffects.ContainsKey(effectType))
            {
                Destroy(_activeEffectIcons[effectType].gameObject);
                effectsToRemove.Add(effectType);
            }
        }
        foreach (var effectType in effectsToRemove)
        {
            _activeEffectIcons.Remove(effectType);
        }

        foreach (var effect in _activeEffects.Values)
        {
            if (_activeEffectIcons.ContainsKey(effect.Type))
            {
                var icon = _activeEffectIcons[effect.Type];
                var text = icon.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = effect.Duration.ToString();
                }
                
            }
            else
            {
                var iconObject = new GameObject(effect.Type.ToString());
                iconObject.transform.SetParent(_effectIconsParent, false);
                var image = iconObject.AddComponent<Image>();
                image.sprite = GetEffectSprite(effect.Type);
                _activeEffectIcons.Add(effect.Type, image);

                var textObject = new GameObject("DurationText");
                textObject.transform.SetParent(iconObject.transform, false);
                var text = textObject.AddComponent<TextMeshProUGUI>();
                text.text = effect.Duration.ToString();
                text.alignment = TextAlignmentOptions.Center;
                text.fontSize = 24;

                var rectTransform = text.GetComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

            }
        }
    }

    private Sprite GetEffectSprite(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.Barrier:
                return _barrierSprite;
            case EffectType.Regeneration:
                return _regenerationSprite;
            case EffectType.Burning:
                return _burningSprite;
            default:
                return null;
        }
    }

    private void InitializeAbilities()
    {
        _abilities = new List<Ability>
        {
            new AttackAbility(),
            new BarrierAbility(),
            new RegenerationAbility(),
            new FireballAbility(),
            new CleanseAbility()
        };
    }

    public Ability ChooseRandomAbility()
    {
        var availableAbilities = _abilities.FindAll(a => !a.IsOnCooldown);

        if (availableAbilities.Count == 0)
        {
            var attackAbility = _abilities.Find(a => a.Name == "Атака");
            if (attackAbility != null)
            {
                return attackAbility;
            }
            else
            {
                return null;
            }
        }

        int index = _random.Next(availableAbilities.Count);
        return availableAbilities[index];
    }

    public void ReduceCooldowns()
    {
        foreach (var ability in _abilities)
        {
            ability.ReduceCooldown();
        }
    }
}
