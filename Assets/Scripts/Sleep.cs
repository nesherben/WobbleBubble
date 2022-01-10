using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sleep : MonoBehaviour
{
	public GameObject sleep;
	public GameObject lightSensor;
	public ProximitySensor Proximity;

    public float DisplayLight { get; private set; }

    void Start()
    {
		Proximity = ProximitySensor.current;
	}

	public void Update()
	{
		if (Proximity != null)
		{
			InputSystem.EnableDevice(Proximity);

			var _proximity = ProximitySensor.current;
			var _dist = _proximity.distance;
			lightSensor.GetComponent<TextMeshProUGUI>().text = _dist.ToString();
        }
        else
        {
			lightSensor.GetComponent<TextMeshProUGUI>().text = "null";

		}

	}

}
