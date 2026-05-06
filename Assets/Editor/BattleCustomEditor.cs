using UnityEngine;
using UnityEditor;

public class BattleCustomEditor : EditorWindow
{
    [MenuItem("Tools/BattleEditor")]
    public static void ShowBattleCustomEditor()
    {
        EditorWindow wnd = GetWindow<BattleCustomEditor>();
        wnd.titleContent = new GUIContent("Battle Editor");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("ClearBattle"))
        {            
            if(FindAnyObjectByType<BattleManager>())
            {
                FindAnyObjectByType<BattleManager>().ClearBattle();
            }
        }
        
        if(GUILayout.Button("DefeatBattle"))
        {
            if(FindAnyObjectByType<BattleManager>())
            {
                FindAnyObjectByType<BattleManager>().GameOver();
            }
        }
    }
}
