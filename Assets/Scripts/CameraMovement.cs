using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 panLimit;
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
