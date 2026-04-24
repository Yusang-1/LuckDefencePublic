using UnityEngine;

public class IdleState : ICharacterState
{
    private static readonly int IsIdleHash = Animator.StringToHash("isIdle");
    private Character character;

    public IdleState(Character character)
    {
        this.character = character;
    }

    public void StateEnter()
    {
        //Debug.Log("Idle State");
        if(character.IsAnimatorNotSet == false)
        {
            character.Animator.SetBool(IsIdleHash, true);            
        }
    }

    public void StateExit()
    {
        if(character.IsAnimatorNotSet == false)
        {
            character.Animator.SetBool(IsIdleHash, false);            
        }
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
