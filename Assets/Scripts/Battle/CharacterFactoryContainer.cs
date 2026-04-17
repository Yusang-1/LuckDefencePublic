using UnityEngine;

public class CharacterFactoryContainer : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;
    
    public AbstractFactory[] Factories => factories;
}
