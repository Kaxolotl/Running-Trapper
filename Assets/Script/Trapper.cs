using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapper : MonoBehaviour {

    [SerializeField]
    Transform _skin;

    public TrapperAnimationControl AnimControl { get { return _anim; } }

    TrapperAnimationControl _anim;

    void Start ()
    {
        _anim = _skin.GetComponent<TrapperAnimationControl>();
        _skin = transform.Find("Skin");
    }
	
	void Update ()
    {
        LookBack(); 
    }

    void LookBack()
    {
        _anim.sprite.flipX = !Input.GetMouseButton(0);

        if (Input.GetMouseButton(0))
            GameManager.Instance.groundSpeed = 0.03f;
        else
            GameManager.Instance.groundSpeed = 0.1f;
    }
}
