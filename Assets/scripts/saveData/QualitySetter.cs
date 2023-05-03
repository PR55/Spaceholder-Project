using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class QualitySetter : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public Dropdown dropdownQuality;

    [SerializeField]
    private bool isPlay;

    private void Awake()
    {
        if (SaveSystem.LoadQuality() != null)
        {
            QualData data = SaveSystem.LoadQuality();
            ChangeQuality(data.qualitySetting);
            if (!isPlay)
            {
                dropdownQuality.value = data.qualitySetting;
            }
        }
        else
        {
            SaveSettings();
            QualData data = SaveSystem.LoadQuality();
            if (!isPlay)
            {
                dropdownQuality.value = data.qualitySetting;
            }
            ChangeQuality(data.qualitySetting);
        }
    }

    // Start is called before the first frame update
    public int currentQualIndex()
    {
        return dropdownQuality.value;
    }

    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];

        
    }

    public void SaveSettings()
    {
        SaveSystem.SaveQuality(this);
    }

}
