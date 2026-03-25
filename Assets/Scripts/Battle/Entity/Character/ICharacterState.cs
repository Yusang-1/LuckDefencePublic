using UnityEngine;

public interface ICharacterState
{
    public void StateEnter();
    public void StateUpdate();
    public void StateExit();
}
