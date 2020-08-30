using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{
    List<List<Task>> schedule;
	public List<List<Task>> Schedule { get
        {
            return schedule;
        }
        set
        {
            schedule = value;
        }
    }
	// Start is called before the first frame update
	void Start()
    {
        schedule = new List<List<Task>>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
