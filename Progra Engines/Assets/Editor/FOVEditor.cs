using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FOV fov = (FOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.radius);

        Vector3 viewAngle = DirectionFromAngle(fov.transform.eulerAngles.y,-fov.angle/2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.radius);

        if(fov.canSeeTraget)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.targetRef.transform.position);
        }
    }

    Vector3 DirectionFromAngle(float eulerY,float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
}
