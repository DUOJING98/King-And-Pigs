using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth;

    public Vector2 minPos;
    public Vector2 maxPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LateUpdate()
    {
        if(target!=null)
        {
            if(transform.position!=target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x,maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y,maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
            }
        }
    }
   public void SetCamPosLimit(Vector2 minpos,Vector2 maxpos)
    {
        minPos = minpos;
        maxPos = maxpos;
    }
}
