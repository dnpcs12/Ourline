using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lr;

    public PlayerController p1;
    public PlayerController p2;

    private int zPos = -1;
    Vector3 p1Pos, p2Pos;

   

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;

        p1Pos = p1.transform.position;
        p2Pos = p2.transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, new Vector3(p1Pos.x,p1Pos.y,zPos));
        lr.SetPosition(1, new Vector3(p2Pos.x, p2Pos.y, zPos));
    }

    public void SetColor(ColorType color)
    {
        print(color);
        lr = GetComponent<LineRenderer>();
        lr.material.color = GameManager.instance.colorTables[(int)color];
    }

}
