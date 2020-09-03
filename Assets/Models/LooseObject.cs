using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseObject : MonoBehaviour
{
	public LooseObject(bool carriable, bool uninstallable, QualityEnum quality, int maxDurability, int currentDurability)
	{
		Carriable = carriable;
		Uninstallable = uninstallable;
		Quality = quality;
		MaxDurability = maxDurability;
		CurrentDurability = currentDurability;
	}

	public bool Carriable { get; set; }
	public bool Uninstallable { get; set; }
	public QualityEnum Quality { get; set; }
	public int MaxDurability { get; set; }
	public int CurrentDurability { get; set; }

	// Start is called before the first frame update
	void Start()
    {

    }

    public void Uninstall()
	{

	}

    // Update is called once per frame
    void Update()
    {

    }

}

public enum QualityEnum
{
    awful,
    poor,
    normal,
    good,
    excelent,
    masterwork,
    legendary
}
