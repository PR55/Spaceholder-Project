using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveComfort(LocomotionSettings LocSet)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if(Application.dataPath+"/UserData" == null)
        {
            Directory.CreateDirectory(Application.dataPath + "/UserData");
        }

        string path = Application.dataPath+"/UserData/comfortSettings.set";

        FileStream stream = new FileStream(path, FileMode.Create);

        LocoData data = new LocoData(LocSet);


        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static LocoData LoadLocomotion()
    {
        string path = Application.dataPath + "/UserData/comfortSettings.set";

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
}
