using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private int numMusicPlayers;
    [SerializeField] private GameObject Audio;
    private void Awake()
    {
        if(!StaticData.musica)
            FindFirstObjectByType<AudioSource>().Pause();
        numMusicPlayers = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None).Length;
        if (numMusicPlayers != 1)
            Destroy(Audio);
        else
            DontDestroyOnLoad(Audio);
    }
}
