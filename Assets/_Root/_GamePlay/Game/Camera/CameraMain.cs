using DG.Tweening;
using System;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    [SerializeField] GameObject objectTargetToFollow;
    public float roomSpeed = 2f;
    public float followSpeed = 5f;
    public float followTargetSpeed = 5f;
    public float offsetTarget = 1f;

    private Camera myCam;
    Vector3 position = Vector3.zero;
    float interpolation = 1f;

    Vector3 camPositionDefaut = Vector3.zero;
    float camSizeDefaut = 8;
    float camSize = 5f;

    bool isFollow = false;
    bool isStartRoom = false;
    bool isFollowTarget = false;

    public void Init()
    {
        myCam = GetComponent<Camera>();
        camSizeDefaut = myCam.orthographicSize;
        camPositionDefaut = myCam.transform.position;
    }

    void LateUpdate()
    {
        if (isStartRoom)
        {
            interpolation = roomSpeed * Time.deltaTime;
            myCam.orthographicSize = Mathf.Lerp(myCam.orthographicSize, camSize, interpolation);
        }
        if (isFollow) 
        {
            interpolation = followSpeed * Time.deltaTime;
            position = this.transform.position;
            if (objectToFollow != null)
            {
                try
                {
                    position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
                    position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
                }
                catch
                {
                    position = this.transform.position;
                }
            }
            this.transform.position = position;
        }

        if (isFollowTarget)
        {
            if (objectTargetToFollow == null) return;
            interpolation = followTargetSpeed * Time.deltaTime;
            position = this.transform.position;

            if (objectTargetToFollow.transform.position.y + offsetTarget <= camPositionDefaut.y)
            {
                position.y = Mathf.Lerp(this.transform.position.y, camPositionDefaut.y, interpolation);
            }
            else
            {
                try
                {
                    position.y = Mathf.Lerp(this.transform.position.y, objectTargetToFollow.transform.position.y + offsetTarget, interpolation);
                }
                catch
                {
                    position = this.transform.position;
                }
            }
            this.transform.position = position;
        }
    }

    public void SetCamSize(float size = 5f)
    {
        camSize = size;
    }

    public void Follow(GameObject objFollow)
    {
        isFollowTarget = false;
        isFollow = true;
        this.objectToFollow = objFollow;
    }
    public void FollowTarget(GameObject obj)
    {
        isFollowTarget = true;
        this.objectTargetToFollow = obj;
    }
    public void UnFollow() 
    {
        isFollow = false;
        this.objectToFollow = null;
    }
    public void Room(GameObject objFollow)
    {
        isStartRoom = true;
        Follow(objFollow);
    }
    public void Defaut()
    {
        isFollow = false;
        isFollowTarget = false;
        isStartRoom = false;
        objectToFollow = null;
        objectTargetToFollow = null;

        this.myCam.transform.position = camPositionDefaut;
        this.myCam.orthographicSize = camSizeDefaut;
    }
}
