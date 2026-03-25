using UnityEngine;

public class AttackState : ICharacterState
{
    private Character character;

    private float cooldown;

    public AttackState(Character character)
    {
        this.character = character;
    }

    public void StateEnter()
    {
        //Debug.Log("Attack State");
        character.AttackTarget = (character.platform.Target as Enemy).GetComponent<IDamagable>();
    }

    public void StateExit()
    {
        
    }

    public void StateUpdate()
    {        
        if(character.IsManaFull)
        {
            character.stateMachine.ChangeState(character.stateMachine.SkillState);
            return;
        }

        cooldown += Time.deltaTime;

        if(cooldown >= character.Data.AttackSpeed)
        {
            character.Attack(character.AttackTarget);            
            cooldown = 0;
        }

        if(character.IsAttackable == false)
        {
            character.stateMachine.ChangeState(character.stateMachine.IdleState);
            return;
        }
    }    
}
