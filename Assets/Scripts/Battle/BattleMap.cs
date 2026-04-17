using UnityEngine;

public class BattleMap : MonoBehaviour
{
    [SerializeField] private Platforms platforms;
    [SerializeField] private BeaconContainer beaconContainer;
    
    public Platforms Platforms => platforms;
    public BeaconContainer BeaconContainer => beaconContainer;
}
