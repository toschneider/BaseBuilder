using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int moveSpeed = 20;
    public int zoomSpeed = 40;
    int minZ = -5, maxZ = -60;
    public WorldController worldController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int y = 0;
        int x = 0;
        int z = 0;

		if (Input.GetKey(KeyCode.W))
		{
            y = 1;
		} else if (Input.GetKey(KeyCode.S))
		{
            y = -1;
		}
		if (Input.GetKey(KeyCode.D))
		{
            x = 1;
		} else if (Input.GetKey(KeyCode.A))
		{
            x = -1;
		}
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            z = 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            z = -1;
        }
        float tmpZ = z * zoomSpeed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos += new Vector3(x * moveSpeed * Time.deltaTime, y * moveSpeed * Time.deltaTime, z * zoomSpeed * Time.deltaTime);
        if (pos.z > minZ) pos.z = minZ;
        else if (pos.z < maxZ) pos.z = maxZ;
        this.transform.position = pos;

    }
}
