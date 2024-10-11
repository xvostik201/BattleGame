using UnityEngine;

public class BarrierAbility : Ability
{
    private int _shieldAmount;
    private int _duration;

    public BarrierAbility()
    {
        Name = "Барьер";
        Cooldown = 4;
        _currentCooldown = 0;
        _shieldAmount = 5;
        _duration = 2;
    }

    public override void Use(Unit user, Unit target)
    {
        if (!IsOnCooldown)
        {
            var barrierEffect = new BarrierEffect(_duration, _shieldAmount);
            user.AddEffect(barrierEffect);
            _currentCooldown = Cooldown;
        }
        else
        {
            Debug.Log($"{Name} находится на перезарядке");
        }
    }
}
