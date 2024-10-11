using UnityEngine;

public class CleanseAbility : Ability
{
    public CleanseAbility()
    {
        Name = "Очищение";
        Cooldown = 5;
        _currentCooldown = 0;
    }

    public override void Use(Unit user, Unit target)
    {
        if (!IsOnCooldown)
        {
            if (user.HasEffect(EffectType.Burning))
            {
                user.RemoveEffect(EffectType.Burning);
            }
            _currentCooldown = Cooldown;
        }
        else
        {
            Debug.Log($"{Name} находится на перезарядке");
        }
    }
}
