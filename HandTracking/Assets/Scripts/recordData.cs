using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class recordData : MonoBehaviour
{
    private float L, W, D, MT, averageSpeed, successRate;
    Dictionary<string, float> record = new Dictionary<string, float>();
    public sceneManager sceneManager;
    public followPath followPath;
    public grabStation grabStation;
    public IOManager ioManager;

    public static int repetitiveTime = 0;
    public static bool isPractice = true;
    public static float reentry;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager.initScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPractice)
        {
            if (followPath.getIsFinish())
            {
                if (sceneManager.directionCount == 12 && sceneManager.blockCount == 5)
                {
                    SceneManager.LoadScene("New Scene");
                }
                sceneManager.updateScene();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Start");
                isPractice = false;
                sceneManager.initScene();
                initRecord();
                repetitiveTime = 0;
            }
        }
        else
        {
            if(followPath.getIsFinish())
            {
                createRecord();
                if(repetitiveTime != 4)
                {
                    followPath.initPathFollow();
                    repetitiveTime++;
                }
                else if(sceneManager.directionCount == 12 && sceneManager.blockCount == 5)
                {
                    SceneManager.LoadScene("New Scene");
                }
                else
                {
                    repetitiveTime = 0;
                    sceneManager.updateScene();
                }
            }
        }
        followPath.followingPath();
    }

    public void initRecord()
    {
        record = new Dictionary<string, float>()
        {
            {"L", 0 },
            {"W", 0 },
            {"D", 0 },
            {"MT", 0 },
            {"repetitiveTime", 0 },
            {"averageSpeed", 0 },
            {"reentry", 0 }
        };
    }

    public void createRecord()
    {
        List<float> timeList = followPath.getTimeList();
        List<Vector3> positionList = followPath.getPositionList();
        float movementTime = timeList[timeList.Count - 1] - timeList[0];
        averageSpeed = followPath.getLength()/movementTime;
        reentry = followPath.getReentry();
        record = new Dictionary<string, float>()
        {
            {"L", sceneManager.getLength() },
            {"W", sceneManager.getWidth() },
            {"D", sceneManager.getDirection() },
            {"MT", movementTime },
            {"repetitiveTime", repetitiveTime },
            {"averageSpeed", averageSpeed },//repetitive time
            {"reentry", reentry }
        };
        ioManager.writeResult(record);
    }
    
}
