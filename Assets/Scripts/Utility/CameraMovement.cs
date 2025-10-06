using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; 
    [SerializeField] private float _fastSpeed = 10f; 

    void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ?  _fastSpeed :  _moveSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

         transform.Translate(move * speed * Time.deltaTime, Space.Self);
    }
}
