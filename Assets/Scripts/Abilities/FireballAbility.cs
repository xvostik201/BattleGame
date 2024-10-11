public class FireballAbility : Ability
{
    private int _immediateDamage;
    private int _duration;
    private int _damagePerTurn;

    public FireballAbility()
    {
        Name = "Огненный шар";
        Cooldown = 6;
        _currentCooldown = 0;
        _immediateDamage = 5;  
        _duration = 5;          
        _damagePerTurn = 1;    
    }

    public override void Use(Unit user, Unit target)
    {
        if (!IsOnCooldown)
        {
            target.TakeDamage(_immediateDamage);

            var burningEffect = new BurningEffect(_duration, _damagePerTurn);
            target.AddEffect(burningEffect);

            _currentCooldown = Cooldown;
        }
    }
}
