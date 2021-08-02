using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform projectorTransform;
    public Projector indicatorProjector;
    public Projector rangeProjector;
    float range;
    Vector3 position;
    /// <summary>
    /// Notes: TargetCircle Scaling works up to _range = 20 , if its higher it breaks.
    ///</summary>
    public void ScaleIndicator(SpellType spellType,float _range = 1 ,float radius = 1){
        
        range = _range;
        switch (spellType)
        {
            case SpellType.Skillshot:
                // per +1 range , + 0.25 on z transform
                indicatorProjector.aspectRatio = _range;
                indicatorProjector.fieldOfView = radius * 10;
                projectorTransform.position = new Vector3(0,projectorTransform.position.y, 0.25f + 0.25f * range);
                break;
            case SpellType.TargetCircle:
                indicatorProjector.fieldOfView = radius * 10;
                rangeProjector.fieldOfView = -0.354118f * Mathf.Pow(_range,2)+ 13.8343f * _range + 0.780462f; //had to do this so the center of the indicator landed at the edge of range indicator at max distance
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// For Skillshot/Cone indicators, rotates the indicator relative to mouse position using the main indicator GameObject as pivot
    ///</summary>
    public void RotateIndicator(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            position = hit.point;
        }
        Quaternion transRot = Quaternion.LookRotation(position - transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transRot, transform.rotation, 0f);
    }
    /// <summary>
    /// For AoE effects, moves the main indicator GameObject to mouse position up to the max range indicator
    ///</summary>
    public void MoveIndicator(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 posUp = new Vector3();
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            position = hit.point;
            posUp = new Vector3(hit.point.x, 0, hit.point.z);
        }

        Vector3 hitPosDir = new Vector3(posUp.x - transform.position.x,0,posUp.z - transform.position.z).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, range * 0.5f);

        Vector3 newHitPos = transform.position + hitPosDir * distance;
        projectorTransform.position = new Vector3(newHitPos.x,projectorTransform.position.y,newHitPos.z);
    }
}
