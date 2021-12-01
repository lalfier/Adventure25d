using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    float parallaxSpeed;    //Paralax movement speed

    float length;
    float startPos;

    void Awake()
    {
        //Get default values
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (Camera.main.transform.position.x * (1 - parallaxSpeed));
        float dist = (Camera.main.transform.position.x * parallaxSpeed);

        //Set X position
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        //Loop left or right
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
