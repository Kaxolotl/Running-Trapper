using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    const int GROUNDSPRITE = 6;

    Transform[] _grounds = new Transform[GROUNDSPRITE]; // 땅 스프라이트 개수
    Transform _currentGround;
    Transform _nextGround;
    SpriteRenderer _currentSprite;
    SpriteRenderer _nextSprite;
    
    void Init()
    {
        GameManager.Instance.groundSpeed = 0.1f;

        // GroundManager오브젝트의 자식인 스프라이트 가진 오브젝트들을 Transform 배열에 넣음
        for (int i = 0; i < GROUNDSPRITE; i++)
        {
            _grounds[i] = transform.Find("DummyGround" + i.ToString());
        }

        // current와 next에 각각 grounds[0],grounds[1] 넣고 시작
        _currentGround = _grounds[0];
        _nextGround = _grounds[1];

        _currentSprite = _currentGround.GetComponent<SpriteRenderer>();
        _nextSprite = _nextGround.GetComponent<SpriteRenderer>();

        // current 오른쪽 끝에 next 위치시키고 켬
        _nextGround.transform.position = _currentSprite.bounds.size * Vector2.right;


        _currentGround.gameObject.SetActive(true);
        _nextGround.gameObject.SetActive(true);
    }

    void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        MovingGround();
    }

    void MovingGround()
    {
        // 땅 이동, 양쪽이 null이 아니면 움직임
        if (_currentGround != null && _nextGround != null)
        {
            _currentGround.transform.Translate(new Vector2(-GameManager.Instance.groundSpeed, 0));
            _nextGround.transform.Translate(new Vector2(-GameManager.Instance.groundSpeed, 0));
        }

        // next가 null이 되면 next에 ground 집어넣는다
        else
        {
            do
            { // 랜덤으로 ground[]에서 넣고 current와 같다면 반복
                _nextGround = _grounds[Random.Range(0, GROUNDSPRITE)];
            } while (_nextGround == _currentGround);

            // next가 null 아니고, next가 current와 같지 않으면 이어붙이고 켬
            if (_nextGround != null && _nextGround != _currentGround)
            {
                _currentSprite = _currentGround.GetComponent<SpriteRenderer>();
                _nextSprite = _nextGround.GetComponent<SpriteRenderer>();               
                
                // 이어붙이고 켬
                _nextGround.transform.position = _currentSprite.bounds.size * Vector2.right;
                _nextGround.gameObject.SetActive(true);
            }
        }
        
        if (_currentSprite != null)
        {
            // current가 화면 밖일 때
            if (_currentSprite.gameObject.activeInHierarchy
                && _currentSprite.bounds.max.x <= Camera.main.ScreenToWorldPoint(Vector2.zero).x)
            {
                // current 끄고, current에 next 넣고, next는 null
                _currentGround.gameObject.SetActive(false);
                _currentGround = _nextGround;
                _nextGround = null;
            }
        }
    }
}
