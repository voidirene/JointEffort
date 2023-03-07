using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffectX, parallaxEffectY;
    [SerializeField] private bool autoScrollX = false, autoScrollY = false;

    private float lengthX, startposX;
    private float lengthY, startposY;

    void Start()
    {
        startposX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        startposY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = cam.transform.position.x * (1 - parallaxEffectX);
        float distanceX = (cam.transform.position.x * parallaxEffectX);
        float desiredXPos = startposX + distanceX;

        float tempY = cam.transform.position.y * (1 - parallaxEffectY);
        float distanceY = (cam.transform.position.y * parallaxEffectY);
        float desiredYPos = startposY + distanceY;

        if (autoScrollX)
        {
            // this will push the object to the left
            desiredXPos = transform.position.x - parallaxEffectX;
        }

        if (autoScrollY)
        {
            // this will push the object down
            desiredYPos = transform.position.y - parallaxEffectY;
        }
        
        transform.position = new Vector2(desiredXPos, desiredYPos);

        if (tempX > startposX + lengthX)
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        if (tempY > startposY + lengthY)
        {
            startposY += lengthY;
        }
        else if (tempY < startposY - lengthY)
        {
            startposY -= lengthY;
        }
    }
}
