using UnityEngine;

public class IdleState : ICharacterState
{
    private Character character;

    public IdleState(Character character)
    {
        this.character = character;
    }

    public void StateEnter()
    {
        //Debug.Log("Idle State");
    }

    public void StateExit()
    {
        
    }

    public void StateUpdate()
    {
        if(character.IsAttackable)
        {
            character.stateMachine.ChangeState(character.stateMachine.AttackState);
            return;
        }
    }
}
