using UnityEngine;

public class SkillState : ICharacterState
{
    private static readonly int IsManaFullHash = Animator.StringToHash("isManaFull");
    private Character character;

    public SkillState(Character character)
    {
        this.character = character;
    }
    public void StateEnter()
    {
        character.UseSkill(character.AttackTarget);
        
        if(character.IsAnimatorNotSet == false)
            character.Animator.SetBool(IsManaFullHash, true);
    }

    public void StateExit()
    {
        if(character.IsAnimatorNotSet == false)
        {
            character.Animator.SetBool(IsManaFullHash, false);
        }
    }

    public void StateUpdate()
    {
        if(character.IsAttackable)
        {
            character.stateMachine.ChangeState(character.stateMachine.AttackState);
            return;
        }
        else
        {
            character.stateMachine.ChangeState(character.stateMachine.IdleState);
            return;
        }
    }
}
