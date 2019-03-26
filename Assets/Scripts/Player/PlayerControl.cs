using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform effect;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotSpeed = 2f;

    private Vector3 targetPos;
    private Vector3 lookAtTarget;
    private Quaternion playerRot;
    private bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isMoving = false;
        }
        if(isMoving)
            MovePlayer();
    }

    private void SetTargetPosition()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))
        {
            targetPos = hit.point;
            targetPos.y = transform.position.y;
            lookAtTarget = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            playerRot.x = 0;
            playerRot.z = 0;
            isMoving = true;
            Instantiate(effect, targetPos, effect.rotation);
        }
    }

    private void MovePlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        if((transform.position - targetPos).magnitude < 2f)
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("wall"))
        {
            isMoving = false;
        }
    }
}
