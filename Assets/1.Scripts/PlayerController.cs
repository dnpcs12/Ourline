using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spr;
    public bool isP1;

    public ColorType colorType;

    public KeyCode switchKey;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    void SwitchColor()
    {
        if (colorType == ColorType.Blue) colorType = ColorType.Red;
        else colorType = ColorType.Blue;

        ApplyColor();
    }

    void ApplyColor()
    {
        spr.color = GameManager.instance.colorTables[(int)colorType];
        if (isP1) GameManager.instance.p1Color = colorType;
        else GameManager.instance.p2Color = colorType;
    }

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        ApplyColor();
    }



    private void Update()
    {
        if (!GameManager.instance.isPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space)) GameManager.instance.Restart();
            return;
        }

        int dirX = 0;
        int dirY = 0;

        if (Input.GetKeyDown(switchKey))
        {
            SwitchColor();
        }

        if (Input.GetKey(rightKey) && transform.position.x < GameManager.instance.maxXY.x) dirX = 1;

        if (Input.GetKey(leftKey) && transform.position.x > GameManager.instance.minXY.x) dirX = -1;

        if (Input.GetKey(upKey) && transform.position.y < GameManager.instance.maxXY.y)
        {
            dirY = 1;
        }
        
        if (Input.GetKey(downKey) && transform.position.y > GameManager.instance.minXY.y)
        {
            dirY = -1;
        }


        transform.Translate(new Vector3(dirX, dirY, 0) * GameManager.instance.playerSpeed * Time.deltaTime);
        
        
    }




}
