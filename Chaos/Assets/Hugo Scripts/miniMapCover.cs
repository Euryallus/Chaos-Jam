using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapCover : MonoBehaviour
{
    public bool uncovered = false;
    public bool repeat = true;

    public SpriteRenderer cover;

    void Update()
    {
        if(uncovered && repeat)
        {
            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, cover.color.a - Time.deltaTime);

            if(cover.color.a <= 0.01f)
            {
                Destroy(cover.gameObject);
                repeat = false;
            }
        }
    }

    public void uncover()
    {
        if(uncovered == false)
        {
            uncovered = true;
        }
    }


}
