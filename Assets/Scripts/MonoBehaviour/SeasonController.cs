using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;




public class SeasonController : MonoBehaviour
{
    private static SeasonController _instance;
    public static SeasonController Instance
    {
        get
        {
            return _instance;
        }
    }
    public int SeasonIterator;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    [SerializeField] private Slider[] SeasonSliders = new Slider[4];
    [SerializeField] private Image StoreBackgroundImage;

    [SerializeField] private Color[] StoreSeasonalColors = new Color[4];
    [SerializeField] private Tilemap SeasonalColoringTileMap;

    private Slider SeasonMeterSlider;


    void Start()
    {
        SeasonIterator = 0;
        SeasonMeterSlider = SeasonSliders[SeasonIterator];
        SeasonMeterSlider.gameObject.SetActive(true);
        StoreBackgroundImage.color = StoreSeasonalColors[SeasonIterator];
        SeasonalColoringTileMap.color = StoreSeasonalColors[SeasonIterator];
    }

    void Update()
    {

    }

    public void NextSeason()
    {
        if(SeasonIterator == 3)
        {
            SeasonIterator = 0;
        }
        else
        {
            SeasonIterator++;
        }
        SeasonMeterSlider.gameObject.SetActive(false);
        SeasonMeterSlider = SeasonSliders[SeasonIterator];
        SeasonMeterSlider.gameObject.SetActive(true);
        SeasonMeterSlider.value = 0.0f;
        StoreBackgroundImage.color = StoreSeasonalColors[SeasonIterator];
        SeasonalColoringTileMap.color = StoreSeasonalColors[SeasonIterator];
    }

    public void UpdateSliderValue(float p_Percentage)
    {
        SeasonMeterSlider.value = p_Percentage;
    }

}
