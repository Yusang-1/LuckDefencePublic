using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    private CharacterData characterData;    
    [SerializeField] private OpenManageCharacterUI openManageCharacterUI;
    
    public void Initialize(CharacterData characterData)
    {
        this.characterData = characterData;
    }
    
    public void OnGameStart()
    {
        if(characterData.isSelectedCharacterFull() == false)
        {
            openManageCharacterUI.OnOpenManageCharacter();
            return;
        }

        SceneChanger.LoadSceneAsync("BattleScene");
    }
}
