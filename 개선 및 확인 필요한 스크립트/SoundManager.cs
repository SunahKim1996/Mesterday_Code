using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static float ConverEffect;
    public static float plusEffect;
    public static float a_submitEffect;
    public static float na_submitEffect;
    public static float clearEffect;
    public static float doorEffect;
    public static float keyboardEffect;
    public static float sighEffect;
    public static float dragEffect;
    public static float normalButtonEffect;
    public static float getEffect;
    public static float getEffect_2;
    public static float swipeEffect;
    public static float doorLockedEffect;
    public static float peopleEffect;
    public static float callingEffect;
    public static float getCallEffect;
    public static float cameraEffect;
    public static float timerEffect;
    public static float paperEffect;
    public static float jingleEffect;

    // Update is called once per frame
    void Awake()
    {
        //Debug.Log("sound volume On");

        ConverEffect = 0.3f;
        plusEffect = 0.3f;
        a_submitEffect = 0.3f;
        na_submitEffect = 0.3f;
        clearEffect = 0.3f;
        doorEffect = 1f;
        doorLockedEffect = 0.4f;
        keyboardEffect = 1f;
        sighEffect = 1f;
        dragEffect = 0.8f;
        normalButtonEffect = 0.4f;
        getEffect = 0.5f;
        getEffect_2 = 0.4f;
        swipeEffect = 0.4f;
        peopleEffect = 0.8f;
        callingEffect = 0.1f;
        getCallEffect = 0.5f;
        cameraEffect = 1f;
        timerEffect = 1f;
        paperEffect = 0.4f;
        jingleEffect = 1f;
    }
}
