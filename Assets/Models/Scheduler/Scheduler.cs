using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{
	/*List<List<Task>> schedule;
	public List<List<Task>> Schedule { get
        {
            return schedule;
        }
        set
        {
            schedule = value;
        }
    }*/

	MyScheduler<List<Task>, Job> schedule;
	public MyScheduler<List<Task>, Job> Schedule
	{
		get
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
		Schedule = new MyScheduler<List<Task>, Job>();
		//	new List<List<Task>>();
		//for (int i = 0; i < Enum.GetNames(typeof(Job)).Length; i++)
		//{
  //          Schedule.Add(new List<Task>());
  //      }
    }
	/// <summary>
	/// returns first Task for given JobCategory
	/// </summary>
	/// <param name="job"></param>
	/// <returns>Task</returns>
	public Task getTask(Job job)
	{
		return schedule[job][0];
	}
    public void AddTask(Task task)
	{
		Schedule[task.TaskJob].Add(task);

	}

    public void AddTasks(List<Task> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
            AddTask(tasks[i]);
		}
	}

    // Update is called once per frame
    void Update()
    {

    }
}
public class WorkPriorities
{
	List<>
}
