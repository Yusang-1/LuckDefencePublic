using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] CharacterData characterData;    
    [SerializeField] private OpenManageCharacterUI openManageCharacterUI;
    
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
