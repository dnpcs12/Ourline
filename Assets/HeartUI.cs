using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    List<Image> hearts = new List<Image>();
    public Sprite emptyHeart;
    public Sprite fullHeart;

    private void Start()
    {
        for(int i =0; i< transform.childCount; i++)
        {
            hearts.Add(transform.GetChild(i).GetComponent<Image>());
        }
        
    }
    public void SetHeart(int life)
    {
        for (int i =0; i < hearts.Count; i++)
        {
            if (i < life)
            {
                hearts[i].sprite = fullHeart;
            }
            else hearts[i].sprite = emptyHeart;
        }
    }
}
