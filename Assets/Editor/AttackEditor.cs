using Gameplay.Player;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Player))]
public class AttackEditor : Editor
{


    private void OnSceneGUI()
    {
        if (target is not Player player) return;

        Handles.color = Color.white;

        Handles.DrawWireArc(player.transform.position, Vector3.forward, Vector3.right, 360, player.AttackDistance);

        Vector3 leftAngle = DirectionFromAngle(player.transform.eulerAngles.z, -player.AttackRadius / 2, player.AttackPivotPoint);
        Vector3 rightAngle = DirectionFromAngle(player.transform.eulerAngles.z, player.AttackRadius / 2, player.AttackPivotPoint);

        Handles.color = Color.red;
        Handles.DrawLine(player.transform.position, player.transform.position + leftAngle * player.AttackDistance);
        Handles.DrawLine(player.transform.position, player.transform.position + rightAngle * player.AttackDistance);
    }


    private Vector3 DirectionFromAngle(float eulerZ, float angleInDegrees, float pivot)
    {
        angleInDegrees += eulerZ;
        return new Vector3(Mathf.Cos((angleInDegrees + pivot) * Mathf.Deg2Rad), Mathf.Sin((angleInDegrees + pivot) * Mathf.Deg2Rad), 0);
    }

}
