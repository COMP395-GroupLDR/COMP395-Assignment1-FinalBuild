using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class WeatherStatus : MonoBehaviour
{
    [SerializeField] private Text weatherStatus;

    private GameObject weatherMaster;
    private string currentWeather;
    // Start is called before the first frame update
    void Start()
    {
        weatherMaster = GameObject.Find("WeatherMaster");
    }

    // Update is called once per frame
    void Update()
    {
        currentWeather = weatherMaster.GetComponent<Weather_Controller>().en_CurrWeather.ToString();
        weatherStatus.text = currentWeather;
    }
}
