// BattleManager.cs
using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private EnemyUnit _enemyUnit;
    [SerializeField] private AbilityUIManager _abilityUIManager;

    void Start()
    {
        var abilities = _playerUnit.GetAbilities();
        _abilityUIManager.Initialize(abilities, OnPlayerAbilitySelected);
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (_playerUnit.CurrentHealth > 0 && _enemyUnit.CurrentHealth > 0)
        {
            yield return StartCoroutine(PlayerTurn());

            if (_enemyUnit.CurrentHealth <= 0)
                break;

            EnemyTurn();
            yield return new WaitForSeconds(1f);

            if (_playerUnit.CurrentHealth <= 0)
                break;

            _playerUnit.UpdateEffects();
            _enemyUnit.UpdateEffects();

            _playerUnit.ReduceCooldowns();
            _enemyUnit.ReduceCooldowns();

            _abilityUIManager.UpdateAbilityButtons();
        }

        if (_playerUnit.CurrentHealth <= 0)
        {
            RestartBattle();
        }
        else
        {
            RestartBattle();
        }
    }

    private IEnumerator PlayerTurn()
    {
        _abilityUIManager.EnableAbilityButtons(true);
        bool abilityUsed = false;
        _abilityUIManager.OnAbilityUsed += () => abilityUsed = true;

        while (!abilityUsed)
        {
            yield return null;
        }

        _abilityUIManager.OnAbilityUsed -= () => abilityUsed = true;
        _abilityUIManager.EnableAbilityButtons(false);
    }

    private void OnPlayerAbilitySelected(Ability ability)
    {
        ability.Use(_playerUnit, _enemyUnit);
    }

    private void EnemyTurn()
    {
        var ability = _enemyUnit.ChooseRandomAbility();
        if (ability != null)
        {
            ability.Use(_enemyUnit, _playerUnit);
        }
    }

    public void RestartBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
