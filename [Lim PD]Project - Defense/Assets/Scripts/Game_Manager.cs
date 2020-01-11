﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Ingame ingame;

    public Object_Pooling object_Pooling;

    private static Game_Manager instance = null;

    public static Game_Manager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        object_Pooling.Initialized();
        ingame.Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
