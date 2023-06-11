using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce = 10f;
    public GameObject gameManager;
    public GameObject lose;
    public GameObject win;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.green);
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy if velocity is greater than 24
        if(collision.relativeVelocity.magnitude > 24f && collision.gameObject.tag != "Win")
        {
            Destroy(collision.transform.parent.gameObject);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // Jump if colliding with ground and bellow player isn't air
        else if ((Physics.Raycast(transform.position + new Vector3(.2f, 0, 0), Vector3.down, 1f) || Physics.Raycast(transform.position + new Vector3(-.2f, 0, 0), Vector3.down, 1f) || Physics.Raycast(transform.position + new Vector3(0, 0, .2f), Vector3.down, 1f) || Physics.Raycast(transform.position + new Vector3(0, 0, -.2f), Vector3.down, 1f)) && collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // Destroy if colliding with enemy
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.transform.DetachChildren();
            Destroy(gameObject);
            Destroy(gameManager);
            lose.SetActive(true);
        }
        // Restart Level if colliding with win
        if(collision.gameObject.CompareTag("Win")) // Restart Level
        {
            win.SetActive(true);
            Destroy(gameManager);
            StartCoroutine(RestartLevel());
        }
    }
}
