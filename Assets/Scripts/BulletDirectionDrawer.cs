using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionDrawer : MonoBehaviour
{
    private const string ground = "Ground";
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform originTransform;
    private float bigDistance = 100f;
    private float smallOffset = 1f;
    private int maxReflectCount = 10;
    private void Start()
    {
        DrawThePath();
    }

    private RaycastHit2D GetHit(Ray2D rayToHit)
    {
        return Physics2D.Raycast(rayToHit.origin, rayToHit.direction, bigDistance, LayerMask.GetMask(ground));
    }


    private void DrawThePath()
    {
        ClearLineRenderer();
        int steps = 0;
        Vector2 direction = originTransform.right;
        Ray2D ray = new Ray2D(originTransform.position + originTransform.right * smallOffset, originTransform.right);
        RaycastHit2D hit = GetHit(ray);
        while (hit.collider != null && hit.collider.gameObject.CompareTag(ground)) 
        {
            steps++;
            if (steps == maxReflectCount)
                break;
            AddPointLineRenderer(hit.point);
            Vector2 n = hit.normal;
            Debug.DrawLine(hit.point, hit.point + n * smallOffset, Color.red);
            direction = direction - 2 * n * Vector2.Dot(direction, n) / Vector2.Dot(n, n);
            ray = new Ray2D(hit.point + smallOffset * direction, direction);
            hit = GetHit(ray);
        } 

        AddPointLineRenderer(hit.point + direction * bigDistance);
    }

    private void ClearLineRenderer()
    {
        lr.positionCount = 1;
        lr.SetPosition(0, originTransform.position);
    }

    private void AddPointLineRenderer(Vector2 point)
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount-1, point);
    }

    private void Update()
    {
        DrawThePath();
    }

}
