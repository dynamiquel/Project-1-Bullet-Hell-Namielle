using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField]
    string saveFilePath = "save.data";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, saveFilePath);

    }

    public void Save(PersistentPlayerData persistentPlayerData)
    {
        FileStream fs = new FileStream(saveFilePath, FileMode.Create);

        var bf = new BinaryFormatter();

        try
        {
            bf.Serialize(fs, persistentPlayerData);
        }
        catch (SerializationException e)
        {
            Debug.LogWarning($"Failed to save file '{saveFilePath}'. Reason: {e}");
        }
        finally
        {
            fs.Close();
            Debug.Log("File saved");
        }
    }

    public PersistentPlayerData Load()
    {
        PersistentPlayerData persistentPlayerData = new PersistentPlayerData();

        if (File.Exists(saveFilePath))
        {
            FileStream fs = new FileStream(saveFilePath, FileMode.Open);

            try
            {
                var bf = new BinaryFormatter();
                persistentPlayerData = (PersistentPlayerData)bf.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Debug.LogWarning($"Failed to load file '{saveFilePath}'. Reason: {e}");
            }
            finally
            {
                fs.Close();
                Debug.Log("File loaded");
            }
        }

        return persistentPlayerData;
    }
}
