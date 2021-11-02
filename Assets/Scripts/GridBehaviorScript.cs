using UnityEngine;
using UnityEngine.InputSystem;

public class GridBehaviorScript : MonoBehaviour
{
    [SerializeField]
    private float interval = 1f;
    [SerializeField]
    private readonly bool debugEnabled = true;
    private Vector3 dudVector3 = new Vector3();

    public void Select(InputAction.CallbackContext context) {
        if (context.started) {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
            if (Physics.Raycast(ray, out hitInfo)) {
                SelectGridPoint(hitInfo.point);
            }
        }
    }

    public void SelectGridPoint(Vector3 gridPoint) {
        var finalPosition = GetGridSpace(gridPoint);
        if (finalPosition != dudVector3) {
           InterfaceBehaviorScript.gm.MoveCursor(finalPosition);
        }
    }

    public Vector3 GetGridSpace(Vector3 position) {
        //Remove offset
        position -= transform.position;
        //Approximate nearest cell
        int xCount = Mathf.RoundToInt(position.x / interval);
//        int yCount = Mathf.RoundToInt(position.y / interval);
        int zCount = Mathf.RoundToInt(position.z / interval);

        //Check if out of bounds.
        if(16 >= xCount || xCount >= 26 || 16 >= zCount || zCount >= 23) {
            return dudVector3; 
        }

        //Set new cell
        Vector3 result = new Vector3(xCount * interval, .5f , zCount * interval);
        //Add back offset and return result
        result += transform.position;
        return result;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (debugEnabled) { 
            for (float x = 17; x < 26; x += interval) {
                for (float z = 17; z < 23; z += interval) {
                    var point = GetGridSpace(new Vector3(x + transform.position.x, 0f, z + transform.position.z));
                    point.y = 0f;
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
