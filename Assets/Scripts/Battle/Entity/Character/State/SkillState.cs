public class SkillState : ICharacterState
{
    private Character character;

    public SkillState(Character character)
    {
        this.character = character;
    }
    public void StateEnter()
    {
        character.UseSkill(character.AttackTarget);
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
        else
        {
            character.stateMachine.ChangeState(character.stateMachine.IdleState);
            return;
        }
    }
}
