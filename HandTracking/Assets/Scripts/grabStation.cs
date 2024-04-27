using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class grabStation : MonoBehaviour
{
    public static float isGrabbed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void trueinGrab()
    {
        isGrabbed = 1;
    }

    public void falseinGrab()
    {
        isGrabbed = 0;
    }
}
