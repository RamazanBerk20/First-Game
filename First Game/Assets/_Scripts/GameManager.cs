using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject clunck;
    public GameObject player;
    public Transform pipe;
    public Vector3 startPoint;
    public float rotateSpeed; 

    private float _xAxis;

    void createClunck()
    {
        // Create a new clunck
        GameObject newClunck = Instantiate(clunck, startPoint, Quaternion.Euler(0, Random.Range(0, 360), 0));
        newClunck.transform.SetParent(pipe);

        // Delete 2 random blocks
        GameObject deleted = newClunck.transform.GetChild(Random.Range(0, 8)).gameObject;
        Destroy(deleted);
        GameObject deleted2 = newClunck.transform.GetChild(Random.Range(0, 7)).gameObject;
        Destroy(deleted2);

        // Create an enemy
        GameObject enemy = newClunck.transform.GetChild(Random.Range(0, 6)).gameObject;
        enemy.tag = "Enemy";
        enemy.GetComponent<Renderer>().material.color = Color.red;

        // Set the start point for the next clunck
        startPoint.y -= 6f;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 11; i++)
        {
            createClunck();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _xAxis = Input.GetAxis("Horizontal");

        player.gameObject.transform.RotateAround(pipe.position, Vector3.up, _xAxis * rotateSpeed * Time.fixedDeltaTime);
    }
}
