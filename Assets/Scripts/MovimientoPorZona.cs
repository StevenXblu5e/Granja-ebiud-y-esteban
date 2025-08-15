using UnityEngine;

public class MovimientoPorZona : MonoBehaviour
{
    public Transform areaMovimiento;
    public float velocidad = 1f;
    public Vector2 destino;
    public SpriteRenderer sr;
    public Vector2 positionInicial; // posicion donde inicia el trayecto
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        NuevoDestino();
        positionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);  

      Debug.Log("Posicion:" + transform.position.x + " Destino " + destino.x );
      
      Vector2 direccion = positionInicial - destino;
      Debug.Log("Direccion:" + direccion.x);

      
      if(direccion.x >= 0){

            sr.flipX = true;       // Ver a la izquierda
        
        }else{
            sr.flipX = false;  // Ver a la derecha
        }

    

      if(Vector2.Distance(transform.position, destino)< 0.1f){
        NuevoDestino ();
        positionInicial = transform.position;
        }

      
    }
    void NuevoDestino(){
        Vector2 centro = areaMovimiento.position;
        Vector2 tamaño = areaMovimiento.localScale;

        float x = Random.Range(centro.x - tamaño.x / 2f, centro.x + tamaño.x / 2f);
        float y = Random.Range(centro.y - tamaño.y / 2f, centro.y + tamaño.y / 2f);

        destino = new Vector2(x, y);
     }   
}
