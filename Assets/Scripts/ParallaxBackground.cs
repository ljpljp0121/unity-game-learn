using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private GameObject cam;


    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //ÉãÏñ»ú¾àÀëÓë±³¾°ÒÆ¶¯¾àÀëµÄ²îÖµ
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //±³¾°ÒÆ¶¯¾àÀë
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        //ÉèÖÃ±³¾°Î»ÖÃ
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}