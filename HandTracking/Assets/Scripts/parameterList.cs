using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parameterList : MonoBehaviour
{
    // value lists
    public static readonly float[] lengthList = { 0.45f, 0.35f };
    public static readonly float[] widthList = { 0.08f, 0.06f, 0.04f };
    public static readonly float[] directionList = { 0, 30, 60, 90, 120, 150,
                                                    180, 210, 240, 270, 300, 330 };
    public static float[] lengthSequence;
    public static float[] widthSequence;
    public static float[] directionSequence;
    public static List<Vector2> block = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        updateBlock();
        updateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //takes an array as input and shuffles its elements using the Fisher-Yates shuffle algorithm
    public static float[] shuffle(float[] array)
    {
        System.Random random = new System.Random();
        // Start from the end of the array and swap each element with a randomly selected element before it
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            // Swap array elements
            float temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }

    public static void updateLength()
    {
        lengthSequence = new float[lengthList.Length];
        lengthSequence = shuffle(lengthList);
    }

    public static void updateWidth()
    {
        widthSequence = new float[widthList.Length];
        widthSequence = shuffle(widthList);
    }

    public static void updateDirection()
    {
        directionSequence = new float[directionList.Length];
        directionSequence = shuffle(directionList);
    }

    public static void updateBlock()
    {
        for (int i = 0; i < lengthList.Length; i++)
        {
            for(int j = 0; j < widthList.Length; j++)
            {
                block.Add(new Vector2(lengthList[i], widthList[j]));
            }
        }
        System.Random random = new System.Random();
        // Start from the end of the array and swap each element with a randomly selected element before it
        for (int i = block.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            // Swap array elements
            Vector2 temp = block[i];
            block[i] = block[randomIndex];
            block[randomIndex] = temp;
        }
    }
}
