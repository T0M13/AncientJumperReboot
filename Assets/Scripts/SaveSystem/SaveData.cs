[System.Serializable]
public class SaveData
{
    public int highscore;
    public int coins;

    public float masterVolume;
    public float musicVolume;
    public float effectVolume;

    public SaveData(int _highscore, int _coins)
    {
        this.highscore = _highscore;
        this.coins = _coins;
    }

    //Options
    public SaveData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        this.masterVolume = _masterVolume;
        this.musicVolume = _musicVolume;
        this.effectVolume = _effectVolume;
    }

}

