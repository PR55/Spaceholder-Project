using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveComfort(LocomotionSettings LocSet)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.dataPath+"comfortSettings.set";

        FileStream stream = new FileStream(path, FileMode.Create);

        LocoData data = new LocoData(LocSet);


        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static void SaveQuality(QualitySetter qualIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.dataPath + "qualitySettings.set";

        FileStream stream = new FileStream(path, FileMode.Create);

        QualData data = new QualData(qualIndex);

        formatter.Serialize(stream, data);

        stream.Close();

    }
    public static LocoData LoadLocomotion()
    {
        string path = Application.dataPath + "comfortSettings.set";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            LocoData data = formatter.Deserialize(stream) as LocoData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

   public static QualData LoadQuality()
    {
        string path = Application.dataPath + "qualitySettings.set";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            QualData data = formatter.Deserialize(stream) as QualData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

}
