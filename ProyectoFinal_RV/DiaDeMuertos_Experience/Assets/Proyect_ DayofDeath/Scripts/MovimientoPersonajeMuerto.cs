using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonajeMuerto : MonoBehaviour
{
    public float velocidad = 5f;
    public float sensibilidadRaton = 2f;
    public float rangoVertical = 60f;

    public Vector3 userDirection;
    private Transform cameraTranform;

    private float rotacionVertical = 0f;
    private CharacterController characterController;


    public string Etiqueta = "Portal";
    public string Bienvenida = "Msg_Bienvenida";
    public string Bailarina = "Msg_Bailarina";
    public string TSI = "Msg_TSI";
    public string Coco = "Msg_Coco";

    public GameObject CalacaBienvenida;
    public GameObject CalacaBaile;
    public GameObject CalacaCoco;
    public GameObject CalacaTSI;

    public GameObject SpawnPosition;

    public GameObject Mundo1;
    public GameObject Player1;

    public GameObject Mundo2;
    public GameObject Player2;

    private bool interact = false;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTranform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        //For user movement OCCULUS
        Vector2 userControl = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float cameraRotation = cameraTranform.eulerAngles.y;
        Vector3 camerarotation = Quaternion.Euler(new Vector3(0, 90, 0)) * cameraTranform.forward;
        userDirection = (camerarotation * Input.GetAxis("Horizontal") + cameraTranform.forward * Input.GetAxis("Vertical")).normalized;
        //userDirection.y = 0f;

        characterController.Move(userDirection * Time.deltaTime * velocidad);

        /*
         * VARIANTE PARA PRUEBAS POR MEDIO DEL MOUSE Y TECLADO
        // Movimiento lateral y frontal
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Rotaci�n en el eje horizontal (giro en el eje Y)
        float rotacionHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;

        // Rotaci�n en el eje vertical (giro en el eje X)
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -rangoVertical, rangoVertical);

        // Calcula el vector de movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical) * velocidad * Time.deltaTime;

        // Aplica el movimiento al objeto
        transform.Rotate(Vector3.up * rotacionHorizontal);

        // Aplica la rotaci�n vertical a la c�mara (o al objeto, dependiendo de tu configuraci�n)
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        // Aplica el movimiento al CharacterController
        characterController.Move(transform.TransformDirection(movimiento));

        // Bloquea y desbloquea el cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
        */
    }

    void OnTriggerEnter(Collider Collision)
    {
        // Verificar si la colisi�n fue con el objeto objetivo
        if (Collision.tag == Etiqueta)
        {
            CambioMundo();
        }

        if(Collision.tag == Bienvenida) {
            CalacaBienvenida.SetActive(true);        
        }else
            CalacaBienvenida.SetActive(false);

        if (Collision.tag == Bailarina){
            CalacaBaile.SetActive(true);
        }else
            CalacaBaile.SetActive(false);

        if (Collision.tag == Coco ){
            CalacaCoco.SetActive(true);
        }else
            CalacaCoco.SetActive(false);

        if (Collision.tag == TSI){
            CalacaTSI.SetActive(true);
        }else
            CalacaTSI.SetActive(false);
    }
    void CambioMundo()
    {
        // Desactivar el mundo activo
        Mundo1.SetActive(false);
        Player1.SetActive(false);
        Player1.transform.position = SpawnPosition.transform.position;
        Player1.transform.rotation = SpawnPosition.transform.rotation;

        // Activar el otro mundo
        Mundo2.SetActive(true);
        Player2.SetActive(true);
        Player2.transform.position = SpawnPosition.transform.position;
        Player2.transform.rotation = SpawnPosition.transform.rotation;
    }

}

