using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOManager : MonoBehaviour
{
    private static string path = @"D:\Kemu Xu\FYP\data\";
    private static string pathTester;

    public int TesterID;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createUserRecord()
    {
        pathTester = path + "Recording" + TesterID + ".txt";
        using (StreamWriter sw = File.CreateText(pathTester))
        {
            sw.WriteLine("TesterID" + "\t" + "L" + "\t" + "W" + "\t" + "D" + "\t" + "MT" + "\t" + "RepetitionTimes" + "\t" + "AverageSpeed" + "\t" + "reentry"); //1是有 0是没有
        }
    }

    public void writeResult(Dictionary<string, float> record)
    {
        using (StreamWriter sw = File.AppendText(pathTester))
        {
            sw.WriteLine(TesterID.ToString() + '\t' + record["L"] + '\t' + record["W"] + '\t' 
                        + record["D"] + '\t' + record["MT"] + '\t' + record["repetitiveTime"] + '\t'
                        + record["averageSpeed"] + '\t' + record["reentry"]);
        }
    }
}
