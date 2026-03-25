using UnityEngine;

public class CharacterStateMachine
{
    ICharacterState currentState;

    private ICharacterState m_AttackState;
    private ICharacterState m_IdleState;
    private ICharacterState m_SkillState;    

    public ICharacterState AttackState => m_AttackState;
    public ICharacterState IdleState => m_IdleState;
    public ICharacterState SkillState => m_SkillState;

    public CharacterStateMachine(Character character)
    {
        m_AttackState = new AttackState(character);
        m_IdleState = new IdleState(character);
        m_SkillState = new SkillState(character);
    }

    public void Initialize(ICharacterState state)
    {
        currentState = state;
        state.StateEnter();
    }

    public void ChangeState(ICharacterState state)
    {
        currentState?.StateExit();

        currentState = state;

        currentState.StateEnter();
    }

    public void StateUpdate()
    {
        currentState?.StateUpdate();
    }
}
