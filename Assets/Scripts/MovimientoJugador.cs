using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public Vector2 entrada;
    public Rigidbody2D rb;
    public float velocidad = 5f;
    private Animator animator;
    public GameObject huevoPrefab;
    public GameObject trigoPrefab;
    public GameObject jitomatePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocity = velocidad * entrada; // 
    }

    public void SembrarTrigo(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            Instantiate(trigoPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SembrarJitomate(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            Instantiate(jitomatePrefab, transform.position, Quaternion.identity);
        }
    }

    public void Movimiento(InputAction.CallbackContext contexto)
    {
        Vector2 valorEntrada = contexto.ReadValue<Vector2>();

        animator.SetBool("estaCaminando", true);

        if (Mathf.Abs(valorEntrada.x) > Mathf.Abs(valorEntrada.y))
        {
            entrada = new Vector2(Mathf.Sign(valorEntrada.x), 0);
        }
        else if (Mathf.Abs(valorEntrada.y) > 0)
        {
            entrada = new Vector2(0, Mathf.Sign(valorEntrada.y));
        }
        else
        {
            entrada = Vector2.zero;
        }

        animator.SetFloat("entradaX", entrada.x);
        animator.SetFloat("entradaY", entrada.y);

        if (contexto.canceled)
        {
            animator.SetBool("estaCaminando", false);
        }
    }

    void OnTriggerEnter2D(Collider2D colision) 
    {
        Debug.Log("TOQUE: " + colision.gameObject.name + " | Tag: " + colision.tag); 

        if (colision.CompareTag("Huevo")) 
        {
        
            GameManager.instancia.SumarHuevo();
            Destroy(colision.gameObject); 
        }
        else if (colision.CompareTag("Trigo")) 
        {
            GameManager.instancia.SumarTrigo();
            Destroy(colision.gameObject); 
        }
        else if (colision.CompareTag("Jitomate")) 
        {
            GameManager.instancia.SumarJitomate();
            Destroy(colision.gameObject); 
        }
    }
}