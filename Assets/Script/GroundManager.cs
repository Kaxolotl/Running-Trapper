using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {
    
    Transform[] grounds = new Transform[3]; // 땅 스프라이트 개수
    Transform currentGround;
    Transform nextGround;
    SpriteRenderer currentSprite;
    SpriteRenderer nextSprite;

    public float groundSpeed;

    void Init()
    {
        // 자식인 땅 이미지 Transform 배열에 넣음
        for (int i = 0; i < transform.childCount; i++)
        {
            grounds[i] = transform.Find("DummyGround" + i.ToString());
        }

        // current는 grounds 배열요소 중 하나
        currentGround = grounds[Random.Range(0, 3)];
        currentGround.gameObject.SetActive(true);

        groundSpeed = 0.1f;
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
        // 땅 이동, next가 null이 아니면 같이 움직임
        if (currentGround != null)
            currentGround.transform.Translate(new Vector2(-groundSpeed, 0));
        if (nextGround != null)
            nextGround.transform.Translate(new Vector2(-groundSpeed, 0));
        
        if (currentGround.transform.position.x <= 0 && nextGround == null) // current가 중간에 오고, next가 비어있으면
        {
            do
            {
                nextGround = grounds[Random.Range(0, 3)];
            } while (nextGround == currentGround);

            currentSprite = currentGround.GetComponent<SpriteRenderer>();
            nextSprite = nextGround.GetComponent<SpriteRenderer>();

            nextGround.transform.position = currentSprite.bounds.size * Vector2.right;
            nextGround.gameObject.SetActive(true);
        }

        if (currentSprite != null)
        { 
            // current가 화면 밖일 때
            if (currentSprite.gameObject.activeInHierarchy 
                && nextSprite != null 
                && currentSprite.bounds.max.x <= Camera.main.ScreenToWorldPoint(Vector2.zero).x)
            {
                // current 끄고, current에 next 넣고, next는 null
                currentGround.gameObject.SetActive(false);
                currentGround = nextGround;
                nextGround = null;
            }
        }
    }
}
