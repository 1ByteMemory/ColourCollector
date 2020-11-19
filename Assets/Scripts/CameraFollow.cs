using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    // public BoxCollider2D mapBounds;

    public Vector2 minRadius;
    public float smoothSpeed = 0.5f;


    // private float xMin, xMax, yMin, yMax;
    // private float camY, camX;
    // private float camOrthsize;
    // private float cameraRatio;
    // private Camera mainCam;
    // private Vector3 smoothPos;
    private Vector2 target;
    private Vector2 cam;


	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawWireCube(transform.position, minRadius);
	}

	private void Start()
    {
        /*
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        // Clamp the camera to the map bounds
        //camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
		//camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);

        target = followTransform.position;
        cam = transform.position;
        float distanceX = target.x - cam.x;
        float distanceY = target.y - cam.y;

        if (Mathf.Abs(distanceX) >= minRadius.x )
        {
            transform.Translate(new Vector2(target.x - cam.x, 0) * smoothSpeed * Time.deltaTime, Space.World);
        }
        if (Mathf.Abs(distanceY) >= minRadius.y)
        {
            transform.Translate(new Vector2(0, target.y - cam.y) * smoothSpeed * Time.deltaTime, Space.World);
        }
    }
}
