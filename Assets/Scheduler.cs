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
    public void AddTask(Task task)
	{
		Schedule[task.TaskJob].Add(task);



		//switch (task.TaskJob)
		//{
		//	case Job.Firefight:
		//		Schedule[0].Add(task);
		//		break;
		//	case Job.Patient:
		//		Schedule[1].Add(task);
		//		break;
		//	case Job.Doctor:
		//		Schedule[2].Add(task);
		//		break;
		//	case Job.Bedrest:
		//		Schedule[3].Add(task);
		//		break;
		//	case Job.HaulPlus:
		//		Schedule[4].Add(task);
		//		break;
		//	case Job.General:
		//		Schedule[5].Add(task);
		//		break;
		//	case Job.Warden:
		//		Schedule[6].Add(task);
		//		break;
		//	case Job.Handle:
		//		Schedule[7].Add(task);
		//		break;
		//	case Job.Entertain:
		//		Schedule[8].Add(task);
		//		break;
		//	case Job.Cook:
		//		Schedule[9].Add(task);
		//		break;
		//	case Job.Hunt:
		//		Schedule[10].Add(task);
		//		break;
		//	case Job.Construct:
		//		Schedule[11].Add(task);
		//		break;
		//	case Job.Grow:
		//		Schedule[12].Add(task);
		//		break;
		//	case Job.Mine:
		//		Schedule[13].Add(task);
		//		break;
		//	case Job.PlantCut:
		//		Schedule[14].Add(task);
		//		break;
		//	case Job.Smith:
		//		Schedule[15].Add(task);
		//		break;
		//	case Job.Tailor:
		//		Schedule[16].Add(task);
		//		break;
		//	case Job.Art:
		//		Schedule[17].Add(task);
		//		break;
		//	case Job.Craft:
		//		Schedule[18].Add(task);
		//		break;
		//	case Job.Haul:
		//		Schedule[19].Add(task);
		//		break;
		//	case Job.Clean:
		//		Schedule[20].Add(task);
		//		break;
		//	case Job.Research:
		//		Schedule[21].Add(task);
		//		break;
		//	case Job.Management:
		//		Schedule[22].Add(task);
		//		break;
		//	case Job.Teach:
		//		Schedule[23].Add(task);
		//		break;
		//}
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
