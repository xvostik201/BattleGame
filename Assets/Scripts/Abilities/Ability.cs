public abstract class Ability
{
    public string Name { get; protected set; }
    public int Cooldown { get; protected set; }
    protected int _currentCooldown;

    public int CurrentCooldown => _currentCooldown;
    public bool IsOnCooldown => _currentCooldown > 0;

    public void ReduceCooldown()
    {
        if (_currentCooldown > 0)
            _currentCooldown--;
    }

    public abstract void Use(Unit user, Unit target);
}
