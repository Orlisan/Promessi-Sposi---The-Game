using UnityEngine;

public class PlayerAlphaMovement : MonoBehaviour
{
    public GameObject camera;
    public float velocita = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void FixedUpdate()
    {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    float y = 0f;

if (Input.GetKey(KeyCode.Space))
{
    y = 1f;  
}
if (Input.GetKey(KeyCode.LeftShift))
{
    y = -1f; 
}
Vector3 pos = new Vector3(x, y, z);
transform.Translate(pos * velocita * Time.fixedDeltaTime);
camera.transform.position = transform.position + new Vector3(0, 2, 1);
    }
}
