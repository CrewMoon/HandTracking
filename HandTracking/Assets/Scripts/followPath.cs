using Meta.WitAi;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class followPath : MonoBehaviour
{
    GameObject targetBall;
    GameObject grabbableObject;
    GameObject grabDetactRight;
    private sceneManager sceneManager;
    private line renderSteeringLine;
    private recordData recordData;

    private float length, width, direction, reentry;
    private Vector3 startpoint, endpoint;
    private List<Vector3> positionList = new List<Vector3>();
    private List<float> timeList = new List<float>();
    private List<bool> grabStationList = new List<bool>();

    private bool isFinish = false;
    //private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        targetBall = GameObject.Find("TargetBall");
        grabbableObject = GameObject.Find("GrabbableObject");
        grabDetactRight = GameObject.Find("GrabDetactRight");
        sceneManager = GameObject.Find("SceneManager").GetComponent<sceneManager>();
        renderSteeringLine = GameObject.Find("SceneManager").GetComponent<line>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void initPathFollow()
    {
        length = sceneManager.getLength();
        width = sceneManager.getWidth();
        direction = sceneManager.getDirection();
        startpoint = sceneManager.getStartpoint();
        positionList = new List<Vector3>();
        timeList = new List<float>();
        grabStationList = new List<bool>();
        isFinish = false;
        grabbableObject.GetComponent<Grabbable>().enabled = true;
    }

    //public void followingPath()
    //{
    //    isGrabbed = getIsGrabbed();
    //    if (positionList.Count == 0)
    //    {
    //        targetBall.transform.position = startpoint;
    //        positionList.Add(startpoint);
    //        timeList.Add(Time.time);
    //        isFinish = false;
    //        grabbableObject.GetComponent<Grabbable>().enabled = true;
    //    }
    //    else
    //    {
    //        endpoint = renderSteeringLine.calculateEndpoint(startpoint, length, direction);
    //        Vector3 actualPosition = targetBall.transform.position;
    //        Vector3 actualDirection = actualPosition - positionList[positionList.Count - 1];
    //        Vector3 pathDirection = (endpoint - startpoint).normalized;
    //        float dotProduct = Vector3.Dot(actualDirection, pathDirection);
    //        if (isGrabbed)
    //        {
    //            grabbableObject.GetComponent<Grabbable>().enabled = true;
    //            if (dotProduct > 0)
    //            {
    //                targetBall.transform.position = positionList[positionList.Count - 1] + dotProduct * pathDirection;
    //                if (Vector3.Distance(targetBall.transform.position, startpoint) <= length)
    //                {
    //                    positionList.Add(targetBall.transform.position);
    //                    timeList.Add(Time.time);
    //                    //Debug.Log("dot: " + dotProduct + ", path direction: " + pathDirection + ", position: " + targetBall.transform.position);
    //                }
    //                else
    //                {
    //                    targetBall.transform.position = endpoint;
    //                    positionList.Add(targetBall.transform.position);
    //                    timeList.Add(Time.time);
    //                    isFinish = true;
    //                    grabbableObject.GetComponent<Grabbable>().enabled = false;
    //                }
    //            }
    //            else
    //            {
    //                targetBall.transform.position = positionList[positionList.Count - 1];
    //            }

    //        }
    //        else
    //        {
    //            targetBall.transform.position = positionList[positionList.Count - 1];
    //            grabbableObject.GetComponent<Grabbable>().enabled = false;
    //        }
    //    }
    //    //Debug.Log("ball position: " + targetBall.transform.position + ", is finished: " + isFinish);
    //}
    public void followingPath()
    {
        if (positionList.Count == 0)
        {
            targetBall.transform.position = startpoint;
            positionList.Add(startpoint);
            isFinish = false;
            grabbableObject.GetComponent<Grabbable>().enabled = true;
        }
        else
        {
            endpoint = renderSteeringLine.calculateEndpoint(startpoint, length, direction);
            Vector3 actualPosition = targetBall.transform.position;
            Vector3 actualDirection = actualPosition - positionList[positionList.Count - 1];
            Vector3 pathDirection = (endpoint - startpoint).normalized;
            float dotProduct = Vector3.Dot(actualDirection, pathDirection);

            if(grabStation.isGrabbed == 1)
            {
                grabbableObject.GetComponent<Grabbable>().enabled = true;
                if (getIsInside())
                {
                    if (dotProduct > 0)
                    {
                        targetBall.transform.position = positionList[positionList.Count - 1] + dotProduct * pathDirection;
                        if (Vector3.Distance(targetBall.transform.position, startpoint) <= length)
                        {
                            positionList.Add(targetBall.transform.position);
                            timeList.Add(Time.time);
                            //Debug.Log("dot: " + dotProduct + ", path direction: " + pathDirection + ", position: " + targetBall.transform.position);
                        }
                        else
                        {
                            targetBall.transform.position = endpoint;
                            positionList.Add(targetBall.transform.position);
                            timeList.Add(Time.time);
                            isFinish = true;
                            grabbableObject.GetComponent<Grabbable>().enabled = false;
                            updateReentry();
                        }
                    }
                    else
                    {
                        targetBall.transform.position = positionList[positionList.Count - 1];
                    }
                }
                else
                {
                    targetBall.transform.position = positionList[positionList.Count - 1];
                }
            }
            else
            {
                targetBall.transform.position = positionList[positionList.Count - 1];
                grabbableObject.GetComponent<Grabbable>().enabled = false;
            }
        }
        //Debug.Log("ball position: " + targetBall.transform.position + ", is finished: " + isFinish);
        grabStationList.Add(grabbableObject.GetComponent<Grabbable>().enabled);
    }
    //public bool getIsGrabbed()
    //{
    //    Transform inner = grabDetactRight.transform;
    //    Transform outer = targetBall.transform;
    //    float distance = Vector3.Distance(inner.position, outer.position);
    //    if(grabStation.isGrabbed == 1)
    //    {
    //        return distance + inner.localScale.x/2 <= outer.localScale.x/2;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void updateReentry()
    {
        reentry = 0;
        if (grabStationList[0] && grabStationList[1] && grabStationList[2] && grabStationList[3] && grabStationList[4])
        {
            reentry++;
        }
        for (int i = 1; i < grabStationList.Count; i++)
        {
            if (!grabStationList[i - 1] && grabStationList[i])
            {
                reentry++;
                //Debug.Log("happen place: " + i);
            }
        }
        reentry--;
    }

    public bool getIsInside()
    {
        Transform inner = grabDetactRight.transform;
        Transform outer = targetBall.transform;
        float distance = Vector3.Distance(inner.position, outer.position);
        return distance + inner.localScale.x / 2 <= outer.localScale.x / 2;
    }

    public bool getIsFinish()
    {
        return isFinish;
    }

    public List<Vector3> getPositionList()
    {
        return positionList;
    }

    public List<float> getTimeList()
    {
        return timeList;
    }

    public float getLength()
    {
        return length;
    }

    public float getReentry()
    {
        return reentry;
    }
}
