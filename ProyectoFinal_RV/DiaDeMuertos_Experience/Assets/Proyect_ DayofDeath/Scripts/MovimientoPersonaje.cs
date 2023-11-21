using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float sensibilidadRaton = 2f;
    public float rangoVertical = 60f;

    private float rotacionVertical = 0f;
    private CharacterController characterController;


    public string Etiqueta = "Portal";
    public GameObject SpawnPosition;

    public GameObject Mundo1;
    public GameObject Player1;

    public GameObject Mundo2;
    public GameObject Player2;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimiento lateral y frontal
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Rotación en el eje horizontal (giro en el eje Y)
        float rotacionHorizontal = Input.GetAxis("Mouse X") * sensibilidadRaton;

        // Rotación en el eje vertical (giro en el eje X)
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -rangoVertical, rangoVertical);

        // Calcula el vector de movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical) * velocidad * Time.deltaTime;

        // Aplica el movimiento al objeto
        transform.Rotate(Vector3.up * rotacionHorizontal);

        // Aplica la rotación vertical a la cámara (o al objeto, dependiendo de tu configuración)
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        // Aplica el movimiento al CharacterController
        characterController.Move(transform.TransformDirection(movimiento));

        // Bloquea y desbloquea el cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    void OnTriggerEnter(Collider portalCollision)
    {
        // Verificar si la colisión fue con el objeto objetivo
        if (portalCollision.tag == Etiqueta)
        {
            CambioMundo();
        }
    }
    void CambioMundo()
    {
        // Desactivar el mundo activo
        Mundo1.SetActive(false);
        Player1.SetActive(false);
        Player1.transform.position = SpawnPosition.transform.position;

        // Activar el otro mundo
        Mundo2.SetActive(true);
        Player2.SetActive(true);
        Player2.transform.position = SpawnPosition.transform.position;
    }
}

