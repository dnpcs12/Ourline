using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{


    SpriteRenderer renderer;


    private ColorType _colorType;

    AudioSource audio;

    private Vector2 outOfScreen = new Vector2(40, -10);

    private float speed;

    private float moveTime = 1f;
    private float moveY;


    public ColorType ColorType
    {
        get => _colorType;

        set
        {
            _colorType = value;
            renderer.material.color = GameManager.instance.colorTables[(int)value];
        }
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        speed = Random.Range(GameManager.instance.minEnemySpeed, GameManager.instance.maxEnmeySpeed);
        ResetColor();
    }

    public void ResetColor()
    {
        ColorType = (ColorType)Random.Range(0, 4);
    }


    private void Update()
    {
        if (!GameManager.instance.isPlay) return;

        moveTime -= Time.deltaTime;
        if (moveTime < 0)
        {
            moveTime = 1f;
            moveY = Random.Range(-GameManager.instance.minEnemySpeed, GameManager.instance.minEnemySpeed);
        }
        if (transform.position.y > GameManager.instance.maxXY.y || transform.position.y < GameManager.instance.minXY.y) moveY *= -1;
        transform.Translate(new Vector3(-speed, moveY, 0) * Time.deltaTime);
    }

    private void MeetLine()
    {
        if (ColorType == ColorType.Black)
        {
            GameManager.instance.life--;
            transform.position = outOfScreen;
            audio.clip = GameManager.instance.BarrierSound;
            StartCoroutine(PlaySound());
        }

        if (ColorType == GameManager.instance.lineColor)
        {
            transform.position = outOfScreen;
            GameManager.instance.Score++;
            audio.clip = GameManager.instance.ScoreSound;
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        
        audio.Play();
        
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
   
        if (collision.tag == "Line")
        {
            MeetLine();
          
        }
        else if (collision.tag == "Barrier")
        {
            if (ColorType != ColorType.Black)
            {
                GameManager.instance.life--;
                transform.position = outOfScreen;
                audio.clip = GameManager.instance.BarrierSound;
                StartCoroutine(PlaySound());

            }
            else gameObject.SetActive(false);

        }
    }

}
