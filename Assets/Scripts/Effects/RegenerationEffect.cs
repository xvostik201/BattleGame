using UnityEngine;

public class RegenerationEffect : Effect
{
    private int _healPerTurn;

    public RegenerationEffect(int duration, int healPerTurn)
        : base(EffectType.Regeneration, duration)
    {
        _healPerTurn = healPerTurn;
    }

    public override void Apply(Unit target)
    {
        Debug.Log($"{target.name} получает эффект регенерации, восстанавливает {_healPerTurn} здоровья каждый ход, длительность {Duration} ходов");
    }

    public override void Remove(Unit target)
    {
        Debug.Log($"{target.name}: эффект регенерации закончился");
    }

    public override void Tick(Unit target)
    {
        base.Tick(target);
        target.Heal(_healPerTurn);
        Debug.Log($"{target.name} восстанавливает {_healPerTurn} здоровья от регенерации");
    }
}
