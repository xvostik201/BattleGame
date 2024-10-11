using UnityEngine;

public class AttackAbility : Ability
{
    public AttackAbility()
    {
        Name = "Атака";
        Cooldown = 0;
        _currentCooldown = 0;
    }

    public override void Use(Unit user, Unit target)
    {
        target.TakeDamage(8);
        _currentCooldown = Cooldown;
    }
}
