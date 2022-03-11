using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{

    public bool hasOptionData;
    public bool hasSaveData;

    public int highscore;
    public int coins;

    [Range(0.0001f, 1f)]
    public float masterVolume = 1f;
    [Range(0.0001f, 1f)]
    public float musicVolume = 1f;
    [Range(0.0001f, 1f)]
    public float effectVolume = 1f;

    private void Awake()
    {
        LoadSaveData();
        LoadOptionsData();
    }

    /// <summary>
    /// Saves the Data
    /// </summary>
    public void SaveSaveData()
    {
        SaveSystem.SaveData(highscore, coins);
        hasSaveData = true;
    }

    /// <summary>
    /// Loads Save Data
    /// </summary>
    public void LoadSaveData()
    {
        SaveData data = SaveSystem.LoadData();
        if (data == null)
        {
            Debug.LogWarning("SaveData empty");
            hasSaveData = false;
            coins = 500;
            highscore = 0;
            SaveSaveData();
        }
        else
        {
            hasSaveData = true;
            highscore = data.highscore;
            coins = data.coins;
        }
    }

    /// <summary>
    /// Sets and saves the audio (masterVolume, musicVolume, effectVolume)
    /// </summary>
    /// <param name="_masterVolume"></param>
    /// <param name="_musicVolume"></param>
    /// <param name="_effectVolume"></param>
    public void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        masterVolume = _masterVolume;
        musicVolume = _musicVolume;
        effectVolume = _effectVolume;
        SaveSystem.SaveOptionsData(masterVolume, musicVolume, effectVolume);
        hasOptionData = true;
    }

    /// <summary>
    /// Loads the audio (masterVolume, musicVolume, effectVolume)
    /// </summary>
    public void LoadOptionsData()
    {
        SaveData data = SaveSystem.LoadOptionsData();
        if (data == null)
        {
            Debug.LogWarning("OptionsData empty");
            hasOptionData = false;
            masterVolume = 0.5f;
            musicVolume = 0.5f;
            effectVolume = 0.5f;
            SaveOptionsData(masterVolume, musicVolume, effectVolume);
        }
        else
        {
            hasOptionData = true;
            masterVolume = data.masterVolume;
            musicVolume = data.musicVolume;
            effectVolume = data.effectVolume;
        }
    }


}




