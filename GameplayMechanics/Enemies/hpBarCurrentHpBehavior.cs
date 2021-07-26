using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBarCurrentHpBehavior : MonoBehaviour
{
    private float startingWidth;
    private Vector3 startingPos;
    private RectTransform sizeTransform;
    public GameObject owner;
    private RectTransform ownerRect;
    private Image currentHPimage;
    void Start()
    {
        sizeTransform = this.GetComponent<RectTransform>();
        startingWidth = sizeTransform.rect.width;
        startingPos = sizeTransform.position;
        ownerRect = owner.GetComponent<RectTransform>();
        currentHPimage = this.GetComponent<Image>();
    }

    public void SetCurrentHp(float hpCoef)
    {
        if (sizeTransform == null) return;
        hpCoef = cutCoef(hpCoef, 0, 1);
        sizeTransform.sizeDelta = new Vector2(startingWidth * hpCoef, sizeTransform.sizeDelta.y);     
        Vector3 newPositionOfBar = ownerRect.position;
        newPositionOfBar.x = newPositionOfBar.x - (startingWidth * (1 - hpCoef) / 2);
        sizeTransform.position = newPositionOfBar;
        currentHPimage.color = getColorViaHP(hpCoef);
    }

    private float cutCoef(float coef, float min, float max)
    {
        if (coef > max)
        {
            coef = max;
        }
        if (coef < min)
        {
            coef = min;
        }
        return coef;
    }

    private Color getColorViaHP(float hpCoef)
    {
        float green = cutCoef(3 * hpCoef, 0, 1);
        float red = cutCoef((-3 * hpCoef) + 3, 0, 1);
        float blue = 0;
        return new Color(red, green, blue);
    }
}
