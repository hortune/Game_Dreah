using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour {
    public float speed = 15f;
    private Vector3 prevPos;

    void Awake()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        Vector3 delta = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
            delta += transform.forward * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            delta -= transform.forward * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            delta -= transform.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            delta += transform.right * speed * Time.deltaTime;

        if (delta.magnitude < 0.1f)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            delta.y = 0;
            gameObject.GetComponent<Rigidbody>().velocity = delta * 100;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation, Time.time * speed * 0.1f);
        Vector3 tmp = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, tmp.y, 0);
        //transform.rotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
    }
}
