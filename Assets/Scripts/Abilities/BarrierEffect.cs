using UnityEngine;

public class BarrierEffect : Effect
{
    public int ShieldAmount { get; private set; }

    public BarrierEffect(int duration, int shieldAmount)
        : base(EffectType.Barrier, duration)
    {
        ShieldAmount = shieldAmount;
    }

    public override void Apply(Unit target)
    {
        Debug.Log($"{target.name} получает барьер на {ShieldAmount} урона, длительность {Duration} ходов");
    }

    public override void Remove(Unit target)
    {
        Debug.Log($"{target.name}: действие барьера закончилось");
    }

    public override void Tick(Unit target)
    {
        base.Tick(target);
    }

    public int AbsorbDamage(int damage)
    {
        int absorbed = Mathf.Min(damage, ShieldAmount);
        ShieldAmount -= absorbed;

        if (ShieldAmount <= 0)
        {
            Duration = 0;
        }

        return damage - absorbed;
    }

}
