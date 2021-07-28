using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform projectorTransform;
    public Projector indicatorProjector;
    public Projector rangeProjector;
    Vector3 position;

    public void ScaleIndicator(SpellType spellType,float range = 1 ,float radius = 1){
        
        switch (spellType)
        {
            case SpellType.Skillshot:
                // per +1 range , + 0.25 on z transform
                indicatorProjector.aspectRatio = range;
                indicatorProjector.fieldOfView = radius * 10;
                projectorTransform.position = new Vector3(0,projectorTransform.position.y, 0.25f + 0.25f * range);
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
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        Quaternion transRot = Quaternion.LookRotation(position - transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transRot, transform.rotation, 0f);
    }
    /// <summary>
    /// For AoE effects, moves the main indicator GameObject to mouse position up to the max range indicator
    ///</summary>
    public void MoveIndicator(Vector3 mousePosition){

    }
}
