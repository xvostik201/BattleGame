using UnityEngine;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;

    protected Dictionary<EffectType, Effect> _activeEffects = new Dictionary<EffectType, Effect>();

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    protected virtual void Awake()
    {
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (_activeEffects.ContainsKey(EffectType.Barrier))
        {
            var barrier = _activeEffects[EffectType.Barrier] as BarrierEffect;
            damage = barrier.AbsorbDamage(damage);

            if (barrier.ShieldAmount <= 0)
            {
                RemoveEffect(EffectType.Barrier);
            }
        }

        _currentHealth -= damage;
        if (_currentHealth < 0)
            _currentHealth = 0;

        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        UpdateHealthUI();
    }

    protected abstract void UpdateHealthUI();

    public void AddEffect(Effect effect)
    {
        if (_activeEffects.ContainsKey(effect.Type))
        {
            _activeEffects[effect.Type].Refresh(effect.Duration);
        }
        else
        {
            _activeEffects.Add(effect.Type, effect);
            effect.Apply(this);
        }
        UpdateEffectIcons();
    }

    public void RemoveEffect(EffectType type)
    {
        if (_activeEffects.ContainsKey(type))
        {
            _activeEffects[type].Remove(this);
            _activeEffects.Remove(type);
            UpdateEffectIcons();
        }
    }

    public void UpdateEffects()
    {
        List<EffectType> effectsToRemove = new List<EffectType>();
        foreach (var effect in _activeEffects.Values)
        {
            effect.Tick(this);
            if (effect.IsExpired)
                effectsToRemove.Add(effect.Type);
        }

        foreach (var effectType in effectsToRemove)
        {
            RemoveEffect(effectType);
        }

        UpdateEffectIcons();
    }

    public bool HasEffect(EffectType effectType)
    {
        return _activeEffects.ContainsKey(effectType);
    }

    protected abstract void UpdateEffectIcons();
}
