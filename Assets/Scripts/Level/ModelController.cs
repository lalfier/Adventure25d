using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField]
    float sizeNearBackground;   //Model scale when near background (Z = 0)
    [SerializeField]
    Canvas displayOverHead;     //Display for text
    [SerializeField]
    float offsetYDisplay;       //Offset display height

    void Update()
    {
        //Change local scale based on Z position
        transform.localScale = Vector3.one * (Mathf.Abs(transform.position.z / 10) + sizeNearBackground);

        //Rotate display towards camera
        displayOverHead.transform.rotation = Camera.main.transform.rotation;

        //Set display position over head depending on scale
        displayOverHead.transform.position = new Vector3(displayOverHead.transform.position.x, transform.localScale.y * offsetYDisplay, displayOverHead.transform.position.z);
    }
}
