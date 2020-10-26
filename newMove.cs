using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class newMove : MonoBehaviour
{
    public int sensitivity= 5;
    public Text Acceleration;
    public Text Temperature;
    public Text Gyroscope;
    public Text Magnetometer;
    public Transform t;
    SerialPort sp = new SerialPort("COM3", 9600);

    private float posX;
    private float posY;
    private float posZ;

    public float Xcal;
    public float Ycal;
    public float Zcal;

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 20;
    }

    void Update()
    {

        try
        {
            
            string UnSplitData = sp.ReadLine();
            string[] SplitData = UnSplitData.Split('|');

            string AccX = SplitData[1];
            string AccY = SplitData[2];
            string AccZ = SplitData[3];
            string Temp = SplitData[4];
            string GyroX = (float.Parse(SplitData[5]) / 10000).ToString();
            string GyroY = (float.Parse(SplitData[6]) / 10000).ToString();
            string GyroZ = (float.Parse(SplitData[7]) / 10000).ToString();
            string MagX = SplitData[8];
            string MagY = SplitData[9];
            string MagZ = SplitData[10];

            /*Acceleration.text = "X: " + AccX + "\nY: " + AccY + "\nZ: " + AccZ;
            Gyroscope.text = "X: " + GyroX + "\nY: " + GyroY + "\nZ: " + GyroZ;
            Magnetometer.text = "X: " + MagX + "\nY: " + MagY + "\nZ: " + MagZ;
            Temperature.text = Temp + "`C";*/

            posX += float.Parse(GyroX) - Xcal;
            posY += float.Parse(GyroY) - Ycal;
            posZ += float.Parse(GyroZ) - Zcal;

            string sentence1 = posX + "_" + posY + "_" + posZ + "|";

            t.rotation = Quaternion.Euler(-posY * 3, -posZ * 7, posX * 0);
        }
        catch
        {
            print("Arduino still starting!");
        }

        string sentence = t.rotation.x + "_" + t.rotation.y + "_" + t.rotation.z + "|";
        print(sentence);
    }
}