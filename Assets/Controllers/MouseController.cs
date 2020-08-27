using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursorPrefab;

    // The world-position of the mouse last frame
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // the world-position start of our left-mouse drag operation
    Vector3 dragStartPosition;
    List<GameObject> dragPreviewGameObjects;

    public float ScrollSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        dragPreviewGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        //UpdateCursor();
        UpdateDragging();
        UpdateCameraMovement();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }
	//void UpdateCursor()
	//{
	//	//Update the circleCursor Position
	//	Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoordinate(currFramePosition);
	//	if (tileUnderMouse != null)
	//	{
	//		Debug.Log(tileUnderMouse != null);
	//		circleCursor.SetActive(true);
	//		Vector3 cursorPosition = new Vector3(tileUnderMouse.x, tileUnderMouse.y, 0);
	//		circleCursor.transform.position = cursorPosition;
	//	}
	//	else
	//	{
	//		circleCursor.SetActive(false);
	//	}
	//}
	void UpdateDragging()
	{
        //Handle left Mouse clicks
        //Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }
        int start_x = Mathf.FloorToInt(dragStartPosition.x);
        int end_x = Mathf.FloorToInt(currFramePosition.x);
        int start_y = Mathf.FloorToInt(dragStartPosition.y);
        int end_y = Mathf.FloorToInt(currFramePosition.y);
        if (end_x < start_x)
        {
            int tmp = end_x;
            end_x = start_x;
            start_x = tmp;
        }

        if (end_y < start_y)
        {
            int tmp = end_y;
            end_y = start_y;
            start_y = tmp;
        }

        //Clean up old drag preview
        while(dragPreviewGameObjects.Count > 0)
		{
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (Input.GetMouseButton(0))
		{
            // Display a preview of the drag area
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.getTileAt(x, y);
                    if (t != null)
                    {
                        // Display the building hint on top of this tile position
                        GameObject go = SimplePool.Spawn(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                    }
                }
            }
        }

        //End Drag
        if (Input.GetMouseButtonUp(0))
        {

            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.getTileAt(x, y);
                    if (t != null)
                    {
                        t.tileType = TileType.Soil;
                    }
                }
            }
        }
    }
    void UpdateCameraMovement()
	{
        //Handle Screendragging
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) //right or middle mouse button
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            diff.z = 0;
            Camera.main.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="coord"></param>
    /// <returns>Tile or null</returns>

}
