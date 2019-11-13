using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

#if UNITY_EDITOR

    static public float debugUIStartX = 10f;
    static public float debugUIStartY = 10f;
    private float height = debugUIStartY;
    private void OnEnable()
    {
        height = debugUIStartY;
        debugUIStartY += 20;
    }

    static public bool isDebugMenuOpen = true;
    void OnGUI()
    {
        isDebugMenuOpen = GUI.Toggle(new Rect(Global.debugUIStartX, height, 100, 20), isDebugMenuOpen, "DebugMeun");
    }

#endif


}
