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
                indicatorProjector.fieldOfView = _range * 10;
                rangeProjector.fieldOfView = _range * 10;
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            position = hit.point;
        }
        Vector3 hitPosDir = transform.position.normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, range);

        Vector3 newHitPos = transform.position + hitPosDir * distance;
        projectorTransform.position = newHitPos;
    }
}
