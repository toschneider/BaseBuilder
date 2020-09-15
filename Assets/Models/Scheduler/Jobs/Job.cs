using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour
{
	public JobEnum JobTitle { get; set; }
	private List<SpecificJob> SpecificJobs;
    public Job(JobEnum jobTilte, List<SpecificJob> specificJobs)
	{
		JobTitle = jobTilte;
		SpecificJobs = specificJobs;
	}
}
public enum JobEnum
{
	Firefight,
	Patient,
	Doctor,
	Bedrest,
	HaulPlus,
	General,
	Warden,
	Handle,
	Entertain,
	Cook,
	Hunt,
	Construct,
	Grow,
	Mine,
	PlantCut,
	Smith,
	Tailor,
	Art,
	Craft,
	Haul,
	Clean,
	Research,
	Management,
	Teach
}
public class SpecificJob
{
	private JobEnum JobTitle;
	public SpecificJob()
	{

	}
}
