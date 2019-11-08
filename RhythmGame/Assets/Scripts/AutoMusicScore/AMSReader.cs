﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AMSReader : MonoBehaviour
{
    Chart chart;
    public string chartFilePath = "Assets\\StreamingAssets\\Charts\\";

    // Start is called before the first frame update
    void Start()
    {
        Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Read()
    {
        var sr = new StreamReader(chartFilePath);
        var buffer = sr.ReadToEnd();
        sr.Close();
        chart = JsonUtility.FromJson<Chart>(buffer);
        Debug.Log("timingCount = " + chart.timing.Length);
        float prevSec = 0;
        foreach(var it in chart.timing)
        {
            if (it > prevSec + chart.NotesInterval)
            {
                //Debug.Log("sec = " + it);
                var note = Game.SingleNotesPool.Instance.GetObject().GetComponent<Game.SingleNote>();
                note.Setup(1, it);
                prevSec = it;
            }
        }
    }
}
