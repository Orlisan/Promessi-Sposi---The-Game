using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAlphaMovement : MonoBehaviour
{
    public float diQuantoLaCameraVieneSpostataAllaRotazioneDellaCameraEffettuataColMouseInTerzaPersona = 1f;
    public float quantitàDiVelocitàPersaQuandoSiRilasciaIlTastoPerSaltareMaTipoCheLaVelocitàVieneMoltiplicataPerUnNumeroDecimale = 0.5f; //AVANTI, GIUDICAMI >:(
    public Rigidbody rb;
    public float jumpPower = 16;
    public GameObject camera;
    public float velocita = 5f;
    public float sensibilita = 2f;
    public Vector3 offset = new Vector3(0, 1, 1);
    public Vector3 thirdyPersonOffset = new Vector3(0, 0, -10);
    public bool thirdyPerson = false;
    public float rotazioneX = 0f;
    float mouseX = 0f;
    float mouseY = 0f;
    public float rotazioneY = 0f;
    private bool isGrounded = false;
    private bool isPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private long cool = 0;
    private bool startCool = false;

    // Update is called once per frame
void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
 void Update()
{
    mouseX = Input.GetAxis("Mouse X") * sensibilita;
    mouseY = Input.GetAxis("Mouse Y") * sensibilita;

    rotazioneY += mouseX;
    rotazioneX -= mouseY;
    rotazioneX = Mathf.Clamp(rotazioneX, -80f, 80f);

    transform.rotation = Quaternion.Euler(0, rotazioneY, 0);
    camera.transform.localRotation = Quaternion.Euler(rotazioneX, 0, 0);
    if(thirdyPerson) {
        
        float rotazioneXRadianti = rotazioneX * Mathf.Deg2Rad;
        float tangenteNonProporzionata = Mathf.Tan(rotazioneXRadianti);
        float tangenteProporzionata = offset.z * tangenteNonProporzionata;
        Vector3 offsetConCorrezioneAltezza = new Vector3(offset.x, offset.y - tangenteProporzionata, offset.z);
        camera.transform.position = transform.position + transform.TransformDirection(offsetConCorrezioneAltezza);
    }
    
        
}
void FixedUpdate()
{
    
    if(cool < 300 && startCool) {
        cool += 50;
    } else
    {
       cool = 0; 
       startCool = false;
    }

float x = Input.GetAxis("Horizontal");
float z = Input.GetAxis("Vertical");
float y = 0f;

if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
{
    rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z); 
}
if (Keyboard.current.spaceKey.wasReleasedThisFrame && rb.linearVelocity.y > 0f)
{
    rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * quantitàDiVelocitàPersaQuandoSiRilasciaIlTastoPerSaltareMaTipoCheLaVelocitàVieneMoltiplicataPerUnNumeroDecimale, rb.linearVelocity.z);
}
if (Input.GetKey(KeyCode.LeftShift) && !isGrounded)
{
    y = -3f; 
}
if(Keyboard.current.ctrlKey.wasPressedThisFrame)
        {
            
            if(!isPressed)
            {
               velocita *= 2; 
            }
            isPressed = true;
        }
if(Keyboard.current.ctrlKey.wasReleasedThisFrame)
        {
            if(isPressed)
            {
                velocita /= 2;
            }
            isPressed = false;
            
        }
        
if (Input.GetKey(KeyCode.F5))
        {if(cool == 0) {
            if(!thirdyPerson)
            {
                offset = thirdyPersonOffset;
                camera.transform.Rotate(0, -60, 0);
            }else
            {
                 offset = new Vector3(0, 1, 1);
                 camera.transform.Rotate(0, 0, 0);
            }
            
            thirdyPerson = !thirdyPerson;
            startCool = true;
           
            if(thirdyPerson) {
                offset = new Vector3(offset.x, offset.y + -mouseY,offset.z + -mouseX);
            }   
        }
        }
Vector3 pos = new Vector3(x, y, z);
transform.Translate(pos * velocita * Time.fixedDeltaTime);
camera.transform.position = transform.position + transform.TransformDirection(offset);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
