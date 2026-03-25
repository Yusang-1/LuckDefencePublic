using UnityEngine;

public class BeaconContainer : MonoBehaviour
{
    [SerializeField] private Transform[] beacons;

    public static Transform[] s_Beacons { get; private set; }

    private void Start()
    {
        s_Beacons = beacons;
    }
}
