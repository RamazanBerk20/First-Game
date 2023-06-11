using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject.transform.GetChild(Random.Range(0,8)).gameObject);
        Destroy(gameObject.transform.GetChild(Random.Range(0,7)).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
