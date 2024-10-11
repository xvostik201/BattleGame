using UnityEngine;

public class RegenerationAbility : Ability
{
    private int _duration;
    private int _healPerTurn;

    public RegenerationAbility()
    {
        Name = "Регенерация";
        Cooldown = 5;
        _currentCooldown = 0;
        _duration = 3;
        _healPerTurn = 2;
    }

    public override void Use(Unit user, Unit target)
    {
        if (!IsOnCooldown)
        {
            var regenerationEffect = new RegenerationEffect(_duration, _healPerTurn);
            user.AddEffect(regenerationEffect);
            _currentCooldown = Cooldown;
        }
        else
        {
            Debug.Log($"{Name} находится на перезарядке");
        }
    }
}
