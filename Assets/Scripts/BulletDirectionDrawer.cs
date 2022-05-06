using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform originTransform;


    private Vector2 GetHit(Vector2 initPoint, Vector2 direction)
    {
        return Physics2D.Raycast(initPoint, direction, 100f, LayerMask.GetMask("Ground")).point;
    }


    void Update()
    {
        Vector2 hitPos = GetHit(originTransform.position, originTransform.up);
        if (hitPos != Vector2.zero)
        {
            lr.enabled = true;
            lr.SetPosition(0, originTransform.position);
            lr.SetPosition(1, hitPos);
        }
        else
        {
            lr.enabled = false;
        }
    }
}
