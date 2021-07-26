using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpriteView : MonoBehaviour
{
    public GameObject rangeSpritePrototype;    
    protected GameObject rangeSprite;
    private void InitRangeSprite()
    {
        rangeSprite = Instantiate(rangeSpritePrototype);
        rangeSprite.SetActive(true);
        rangeSprite.transform.SetParent(this.gameObject.transform);
        rangeSprite.transform.position = GetRangeSpritePosition();
    }

    public void UpdateRangeSprite(float newRange)
    {
        if (rangeSprite == null) 
        {
            InitRangeSprite();
        }
        TransformSpriteScaleWithRange(newRange);
    }

    virtual protected void TransformSpriteScaleWithRange(float newRange)
    {
        float rangeCoef = 2 / this.gameObject.transform.localScale.x;
        Vector3 newScale = new Vector3(newRange * rangeCoef, newRange * rangeCoef, newRange * rangeCoef);
        rangeSprite.transform.localScale = newScale;
    }

    virtual protected Vector3 GetRangeSpritePosition()
    {
        return new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z);
    }
}
