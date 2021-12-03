using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;  //Player object to follow
    [SerializeField]
    float timeOffset;   //Time delay
    [SerializeField]
    float posOffset;    //Offset in X axis
    [SerializeField]
    float rightLimit;   //Limit camera to right side of screen
    [SerializeField]
    float leftLimit;    //Limit camera to left side of screen

    Vector3 velocity;

    void Update()
    {
        //Set start and end pos and smooth update camera pos
        Vector3 startPos = transform.position;
        Vector3 endPos = player.transform.position;

        endPos.x += posOffset;
        endPos.y = startPos.y;
        endPos.z = startPos.z;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        //Clamp camera to screen size
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit/Camera.main.aspect, rightLimit / Camera.main.aspect), transform.position.y, transform.position.z);
    }
}
