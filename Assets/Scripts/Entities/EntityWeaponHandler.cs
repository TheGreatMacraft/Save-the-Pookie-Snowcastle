using UnityEngine;
[DisallowMultipleComponent]

public sealed class EntityWeaponHandler
    : MonoBehaviour
{
    [SerializeField] private GunComponent gun;
    
    private ActionExecution weaponActions;

    private void Awake()
    {
        Weapon weapon = gun;

        weaponActions = new MultipleActionExecutions(
            weapon.DefaultAttack()
            //weapon.SupportAction(),
            //weapon.HeavyAttack(),
            //weapon.Ability()
        );
    }

    private void Update()
    {
        gun.DefaultAttack().Execute();
        weaponActions.Execute();
    }
}