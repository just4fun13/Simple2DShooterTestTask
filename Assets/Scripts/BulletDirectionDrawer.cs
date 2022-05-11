using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionDrawer : MonoBehaviour
{
    private const string ground = "Ground";
    private const string myTag = "Enemy";
    private static Transform originTransform;
    private static float bigDistance = 100f;
    private static float smallOffset = 1f;
    private static int maxReflectCount = 10;
    private static LayerMask everythingMask = ~0;

    private static RaycastHit2D GetHit(Ray2D rayToHit)
    {
        return Physics2D.Raycast(rayToHit.origin, rayToHit.direction, bigDistance, everythingMask);
    }

    public static void SetOriginTransform(Transform tr)
    {
        originTransform = tr;
    }

    public static bool CanHitRicochet()
    {
        int steps = 0;
        Vector2 direction = originTransform.right;
        Ray2D ray = new Ray2D(originTransform.position + originTransform.right * smallOffset, originTransform.right);
        RaycastHit2D hit = GetHit(ray);
        while (hit.collider != null ) 
        {
            if (!hit.collider.gameObject.CompareTag(ground))
            {
                if (hit.collider.gameObject.CompareTag(myTag))
                    return false;
                else
                    return true;
            }
            steps++;
            if (steps == maxReflectCount)
                break;
            Vector2 n = hit.normal;
            Debug.DrawLine(hit.point, hit.point + n * smallOffset, Color.red);
            direction = direction - 2 * n * Vector2.Dot(direction, n) / Vector2.Dot(n, n);
            ray = new Ray2D(hit.point + smallOffset * direction, direction);
            hit = GetHit(ray);
        }
        return false;
    }

}
