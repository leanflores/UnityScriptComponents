using UnityEngine;
using System.Collections;
using CnControls;

public class FollowPlayer : MonoBehaviour {

    public Transform p_Transform;
    public Transform h_Transform;
    float cameraFollowSpeed = 5f;
    public Vector3 pos_offset;
    public bool highCam = false;

    Quaternion lowCamAngle;
    Quaternion highCamAngle;
    Vector3 lowCamOffset;
    Vector3 highCamOffset;

    Vector3 initCamOffset;
    Vector3 camOffset;
    Quaternion camAngle;

    float distance;
    float[] camZoomMarkers;
    float camZoomFactor;

    void Awake () {
        //Debug.Log("High cam offset \n x: " +
        //        offset.x + "\ny: " +
        //        offset.y + "\nz: " +
        //        offset.z);

        lowCamAngle = new Quaternion(0.26f, 0f, 0f, .97f);
        lowCamOffset = new Vector3(-0.75f, 14.8f, -21.8f);

        highCamAngle = new Quaternion(0.36f, 0f, 0f, 0.93f);
        highCamOffset = new Vector3(-0.30f, 20.9f, -20.4f);

        initCamOffset = (highCam) ? highCamOffset : lowCamOffset;
        camAngle = (highCam) ? highCamAngle : lowCamAngle;
        //Debug.Log("High cam Quaternion \n x: " +
        //        transform.rotation.x + "\ny: " +
        //        transform.rotation.y + "\nz: " +
        //        transform.rotation.z + "\nw: " +
        //        transform.rotation.w);

    }

    void Start()
    {
        transform.rotation = camAngle;
        //transform.position = p_Transform.position + initCamOffset;
        camOffset = initCamOffset;
        cameraFollowSpeed = 1f;
        if (highCam)
        {
            camZoomMarkers = new float[] { 20, 40, 50};
            camZoomFactor = 2f;
        }
        else
        {
            camZoomMarkers = new float[] { 15, 25, 50, 75 };
            camZoomFactor = 2f;
        }

    }


    void FixedUpdate () {


        

        distance = (p_Transform.position.z - h_Transform.position.z);
        //Debug.Log("Distance: " + distance);

        bool zooming = false;
        if (LevelState.levelManager.levelStart) {
            
            for (int i=0; i < camZoomMarkers.Length; i++)
            {
                if (distance >= camZoomMarkers[i])
                {
                    camOffset = initCamOffset * (float) (i + camZoomFactor);
                    zooming = true;
                    cameraFollowSpeed = 2f;
                }
            }
            if (!zooming)
            {
                camOffset = initCamOffset;
            }
        }
        Vector3 newPosition = p_Transform.position + camOffset;
        transform.position = Vector3.Lerp(transform.position, newPosition, cameraFollowSpeed * Time.deltaTime);

    }



}
