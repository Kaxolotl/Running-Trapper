using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private GameManager()
    {
    }
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
                instance = new GameManager();
            return instance;
        }
    }

    public float groundSpeed;
}
