using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{

    public float maxH;
    public float minH;
    public GameObject enemyPrefabs;
    
    private List<Enemy> enemies = new List<Enemy>();

    public Transform poolTransform;
    public int poolSize;

  

    private float genTime;

    private int _curNum;
    public int curNum
    {
        get => _curNum;
        set
        {
            _curNum = value % poolSize;
        }
    }

    private void Start()
    {
        genTime = 0;
        for(int i=0; i <poolSize; i++)
        {
            enemies.Add(Instantiate(enemyPrefabs, poolTransform).GetComponent<Enemy>());
        }
    }

    private void Update()
    {

        if (!GameManager.instance.isPlay) return;

        genTime -= Time.deltaTime;

        if(genTime < 0)
        {
            genTime = Random.Range(GameManager.instance.minGenTime, GameManager.instance.maxGenTime);
            Generate();
        }
        
    }

    public void Generate()
    {
        
        float yPos = Random.Range(minH, maxH);
        enemies[curNum].transform.position = new Vector3(transform.position.x, yPos, transform.position.y);
        enemies[curNum].gameObject.SetActive(true);
       
        curNum += 1;
       

    }
}
