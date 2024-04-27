using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    GameObject targetBall;
    LineRenderer steeringLine;
    parameterList parameterList;
    private float lengthOffset = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setTargetBall(Vector3 startpoint, float width)
    {
        targetBall = GameObject.Find("TargetBall");
        targetBall.transform.position = startpoint;
        targetBall.transform.localScale = new Vector3(width, width, width);
    }

    public Vector3 calculateEndpoint(Vector3 startpoint, float length, float direction)
    {
        float theta = Mathf.PI / 180 * direction;

        Vector3 endpoint = Vector3.zero;
        endpoint.x = startpoint.x + length * Mathf.Cos(theta);
        endpoint.y = startpoint.y + length * Mathf.Sin(theta);
        endpoint.z = startpoint.z;
        return endpoint;
    }

    public void initSteeringLine()
    {
        steeringLine = this.GetComponent<LineRenderer>();
        steeringLine.startWidth = 0.01f;
        steeringLine.endWidth = 0.01f;
        steeringLine.positionCount = 2;

    }

    public void updateSteeringLine(Vector3 startpoint, Vector3 endpoint)
    {
        steeringLine.SetPosition(0, startpoint);
        steeringLine.SetPosition(1, endpoint);
    }

    public void updateScene(float length, float width, float direction, Vector3 startpoint)
    {
        setTargetBall(startpoint, width);
        initSteeringLine();
        Vector3 endpoint = calculateEndpoint(startpoint, length + lengthOffset, direction);
        updateSteeringLine(startpoint, endpoint);
    }

    public void initBackground(Vector3 startpoint)
    {
        parameterList = this.GetComponent<parameterList>();
        for(int i = 0; i < parameterList.directionList.Length; i++)
        {
            LineRenderer iLine = GameObject.Find(parameterList.directionList[i] + "DegreeLine").GetComponent<LineRenderer>();
            iLine.SetPosition(0, startpoint);
            Vector3 endpoint = calculateEndpoint(startpoint, parameterList.lengthList[0], parameterList.directionList[i]);
            iLine.SetPosition(1, endpoint);
        }
    }
}
