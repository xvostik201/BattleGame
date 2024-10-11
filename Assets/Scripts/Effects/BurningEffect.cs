using UnityEngine;

public class BurningEffect : Effect
{
    private int _damagePerTurn;

    public BurningEffect(int duration, int damagePerTurn)
        : base(EffectType.Burning, duration)
    {
        _damagePerTurn = damagePerTurn;
    }

    public override void Apply(Unit target)
    {
        Debug.Log($"{target.name} получает эффект горения, наносит {_damagePerTurn} урона каждый ход, длительность {Duration} ходов");
    }

    public override void Remove(Unit target)
    {
        Debug.Log($"{target.name}: эффект горения закончился");
    }

    public override void Tick(Unit target)
    {
        base.Tick(target);
        target.TakeDamage(_damagePerTurn);
        Debug.Log($"{target.name} получает {_damagePerTurn} урона от горения");
    }
}
