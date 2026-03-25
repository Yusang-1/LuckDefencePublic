using UnityEngine;
using System.Runtime.InteropServices;

public class WebManager : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void HelloString(string str);
    
    [DllImport("__Internal")]
    private static extern void Hello();
    #endif

    void Start() {
        #if UNITY_WEBGL && !UNITY_EDITOR
        HelloString("Hello World!");
        #endif
    }
    
    public void AlertHello()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        Hello();
        #endif
    }
}
