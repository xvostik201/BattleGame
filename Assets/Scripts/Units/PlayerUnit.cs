// PlayerUnit.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerUnit : Unit
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _effectIconsParent;
    [SerializeField] private Sprite _barrierSprite;
    [SerializeField] private Sprite _regenerationSprite;
    [SerializeField] private Sprite _burningSprite;

    private Dictionary<EffectType, EffectIcon> _activeEffectIcons = new Dictionary<EffectType, EffectIcon>();
    private List<Ability> _abilities;

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
        List<EffectType> effectsToRemove = new List<EffectType>();
        foreach (var effectType in new List<EffectType>(_activeEffectIcons.Keys))
        {
            if (!_activeEffects.ContainsKey(effectType))
            {
                Destroy(_activeEffectIcons[effectType].Icon.gameObject);
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
                _activeEffectIcons[effect.Type].DurationText.text = effect.Duration.ToString();
            }
            else
            {
                GameObject iconObject = new GameObject(effect.Type.ToString());
                iconObject.transform.SetParent(_effectIconsParent, false);

                Image image = iconObject.AddComponent<Image>();
                image.sprite = GetEffectSprite(effect.Type);

                GameObject textObject = new GameObject("DurationText");
                textObject.transform.SetParent(iconObject.transform, false);

                TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
                text.text = effect.Duration.ToString();
                text.alignment = TextAlignmentOptions.Center;
                text.fontSize = 24;

                RectTransform rectTransform = text.GetComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

                EffectIcon effectIcon = new EffectIcon
                {
                    Icon = image,
                    DurationText = text
                };
                _activeEffectIcons.Add(effect.Type, effectIcon);
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

    public List<Ability> GetAbilities()
    {
        return _abilities;
    }

    public void ReduceCooldowns()
    {
        foreach (var ability in _abilities)
        {
            ability.ReduceCooldown();
        }
    }

    private class EffectIcon
    {
        public Image Icon;
        public TextMeshProUGUI DurationText;
    }
}
