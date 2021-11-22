using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ColorType { Red, Blue, Purple, Black };

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Color[] colorTables = { Color.red, Color.blue, Color.magenta, Color.black };

    public Line2d line;

    public Vector2 limitEnemySpeed;
    public float maxEnmeySpeed;
    public float minEnemySpeed;
    

    public Vector2 limitGenTime;
    public float minGenTime;
    public float maxGenTime;
    
    public float playerSpeed;

    private AudioSource audSorce;

    public AudioClip ScoreSound;
    public AudioClip BarrierSound;
    public AudioClip backgroundMusic;
    public AudioClip DieMusic;

    private ColorType _p1Color;
    private ColorType _p2Color;
    private ColorType _lineColor;

    public Text ScoreText;
    public Text LifeText;
    public HeartUI heartUI;
    public Text BestScoreText;
    public GameObject PausePanle;

    public bool isPlay = true;

    public bool isDie = false;
    bool isGameOver = false;

    public GameObject gameOverText;

    private int _score;

    public Vector2 minXY;

    public Vector2 maxXY;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreText.text = "Score :" + _score.ToString();
            if(Score % 5 == 0)
            {
                maxGenTime -= 0.07f;
                minGenTime -= 0.07f;
                maxEnmeySpeed += 0.4f;
                minEnemySpeed += 0.4f;

                if (minGenTime < limitGenTime.x) minGenTime = limitGenTime.x;
                if (maxGenTime < limitGenTime.y) maxGenTime = limitGenTime.y;

                if (minEnemySpeed > limitEnemySpeed.x) minEnemySpeed = limitEnemySpeed.x;
                if (maxEnmeySpeed > limitEnemySpeed.y) maxEnmeySpeed = limitEnemySpeed.y;
            }
            if(Score > BestScore)
            {
                PlayerPrefs.SetInt("BestScore", Score);
                BestScore = Score;
            }
        }
    }

    private int _bestScore;
    public int BestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            BestScoreText.text = "Best Score : "+ _bestScore.ToString();
        }
    }

    private int _life = 5;
    public int life
    {
        get => _life;
        set {
            _life = value;

            if (life <= 0) Die();
            heartUI.SetHeart(life);
                //LifeText.text = "Life :" + _life.ToString();
        }
    }

    public ColorType p1Color
    {
        get => _p1Color;
        set
        {
            _p1Color = value;
            SetLineColor();
            
        }
    }
    public ColorType p2Color
    {
        get => _p2Color;
        set
        {
            _p2Color = value;
            SetLineColor();
        }
    }
    public ColorType lineColor
    {
        get => _lineColor;
        set
        {
            _lineColor = value;
            line.SetColor(_lineColor);
        }
    }
    private void Update()
    {
        if (!isGameOver &&Input.GetKeyDown(KeyCode.Escape))
        {
            isPlay = !isPlay;
            PausePanle.SetActive(!isPlay);
        }
    }


    public void Die()
    {
        isDie = true;
        isPlay = false;
        isGameOver = true;
        gameOverText.SetActive(true);
        //audSorce.clip = DieMusic;
       // audSorce.Play();

    }

    public void SetLineColor()
    {
        print(p1Color + " " + p2Color + " ");
        if(p1Color == p2Color && p1Color == ColorType.Blue)
        {
            lineColor = ColorType.Blue;
        }
        else if(p1Color == p2Color && p1Color == ColorType.Red)
        {
            lineColor = ColorType.Red;
        }
        else if(p1Color != p2Color)
        {
            lineColor = ColorType.Purple;
        }

    }

    public void Resume()
    {
        PausePanle.SetActive(false);
        isPlay = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
    private void Start()
    {
        audSorce = GetComponent<AudioSource>();
        audSorce.clip = backgroundMusic;
        audSorce.Play();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        
    }

   


}
