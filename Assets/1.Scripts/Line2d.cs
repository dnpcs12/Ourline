using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line2d : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    SpriteRenderer spr;

    public float yScale = 0.8f;

    float distance;
    float dir;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(p1.position, p2.position);

        Vector2 v = p1.position - p2.position;

        transform.position = (p1.position + p2.position) / 2;
        transform.localScale = new Vector3(distance, yScale, 1);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);

    }
    
    public void SetColor(ColorType color)
    {
        print("line color:"+color);
        spr.color = GameManager.instance.colorTables[(int)color];
    }
}
