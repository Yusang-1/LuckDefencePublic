using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static List<Entity> Characters = new List<Entity>();
    
    public static void Activated(Entity entity)
    {
        Characters.Add(entity);
    }

    public static void Deactivated(Entity entity)
    {
        Characters.Remove(entity);        
    }

    public void OnDeactivateAllCharacters()
    {
        foreach(var character in Characters)
        {
            character.gameObject.SetActive(false);
        }
        Characters.Clear();
    }
}
