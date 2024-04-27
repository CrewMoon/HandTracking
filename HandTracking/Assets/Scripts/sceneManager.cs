using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    private float L, W, D;
    public static int lengthCount = 0;
    public static int widthCount = 0;
    public static int directionCount = 0;
    public static int blockCount = 0;
    public static int repetitiveTime = 5;
    public static Vector3 startpoint = new Vector3(0, 1.0f, 0.35f);
    public static Vector3 endpoint;

    public line renderSteeringLine;
    public followPath followPath;
    public IOManager ioManager;

    public void initScene()
    {
        lengthCount = 0;
        widthCount = 0;
        directionCount = 0;
        blockCount = 0;
        renderSteeringLine.initBackground(startpoint);
        updateScene();
        ioManager.createUserRecord();
        
    }

    public void updateScene()
    {
        //Debug.Log("current state: L-" + lengthCount + ", W-" + widthCount + ", D-" + directionCount);
        //Debug.Log("current value: L-" + L + ", W-" + W + ", D-" + D + ", repeat-" + repetitiveTime);
        //Debug.Log("sequence value: L-" + lengthCount + ", W-" + widthCount + ", D-" + directionCount + ", repeat-" + repetitiveTime);
        //Debug.Log("remaining value: L-" + ((lengthCount + 1) % 2) + ", W-" + ((widthCount + 1) % 3) + ", D-" + ((directionCount + 1) % 12));
        if ((directionCount % 12) == 0 && directionCount != 0)
        {
            directionCount = 0;
            blockCount++;
            parameterList.updateDirection();
            Debug.Log("This is the block" + blockCount);
        }
        if ((blockCount % 6) == 0 && blockCount != 0)
        {
            blockCount = 0;
            parameterList.updateBlock();
        }
        L = parameterList.block[blockCount].x;
        W = parameterList.block[blockCount].y;
        D = parameterList.directionSequence[directionCount];
        //L = 0.35f;
        //W = 0.06f;
        //D = 120; 
        renderSteeringLine.updateScene(L, W, D, startpoint);
        followPath.initPathFollow();
        directionCount++;

    }

    public float getLength()
    {
        return L;
    }

    public float getWidth()
    {
        return W;
    }

    public float getDirection()
    {
        return D;
    }

    public Vector3 getStartpoint()
    {
        return startpoint;
    }

}
