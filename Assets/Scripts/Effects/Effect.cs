using UnityEngine;

public enum EffectType
{
    Barrier,
    Regeneration,
    Burning
}

public abstract class Effect
{
    public EffectType Type { get; private set; }
    public int Duration { get; protected set; }
    public bool IsExpired => Duration <= 0;

    public Effect(EffectType type, int duration)
    {
        Type = type;
        Duration = duration;
    }

    public virtual void Apply(Unit target)
    {
    }

    public virtual void Remove(Unit target)
    {
    }

    public virtual void Tick(Unit target)
    {
        Duration--;
    }

    public void Refresh(int duration)
    {
        Duration = duration;
    }
}
