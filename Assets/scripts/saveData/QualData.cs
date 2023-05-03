using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QualData
{
    public int qualitySetting;

    public QualData(QualitySetter qualitySettings)
    {
        qualitySetting = qualitySettings.currentQualIndex();
    }
}
