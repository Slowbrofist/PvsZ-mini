using UnityEngine;
using UnityEngine.InputSystem;

public class GridBehaviorScript : MonoBehaviour
{
    [SerializeField]
    private float interval = 1f;
    [SerializeField]
    private readonly bool debugEnabled = true;

    public void Select(InputAction.CallbackContext context) {
        if (context.started) {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
            if (Physics.Raycast(ray, out hitInfo)) {
                SelectGridPoint(hitInfo.point);
            }
        }
    }

    private void SelectGridPoint(Vector3 gridPoint) {
        var finalPosition = GetGridSpace(gridPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }

    public Vector3 GetGridSpace(Vector3 position) {
        //Remove offset
        position -= transform.position;
        //Approximate nearest cell
        int xCount = Mathf.RoundToInt(position.x / interval);
        int yCount = Mathf.RoundToInt(position.y / interval);
        int zCount = Mathf.RoundToInt(position.z / interval);
        //Set new cell
        Vector3 result = new Vector3(xCount * interval, yCount * interval, zCount * interval);
        //Add back offset and return result
        result += transform.position;
        return result;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (debugEnabled) { 
            for (float x = 0; x < 40; x += interval) {
                for (float z = 0; z < 40; z += interval) {
                    var point = GetGridSpace(new Vector3(x + transform.position.x, 0f, z + transform.position.z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
