using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                    Debug.LogWarning(string.Format("{0}.. 없음...", typeof(GameManager).ToString()));

            }
            return _instance;
        }
    }

    public float groundSpeed = 0;
    public float walkedDistance = 0;

    private void Start()
    {
        InvokeRepeating("DistanceCheck", 0, 0.5f);
    }
    private void Update()
    {
        walkedDistance += groundSpeed;
    }
    void DistanceCheck()
    {
        Debug.Log(walkedDistance);
    }
}
