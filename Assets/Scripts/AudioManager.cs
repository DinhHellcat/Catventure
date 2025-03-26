using UnityEngine;
using UnityEngine.SceneManagement; // Thêm để lắng nghe sự kiện scene

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Nhạc nền
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip level1Music;
    [SerializeField] private AudioClip level2Music;
    [SerializeField] private AudioClip level3Music;

    // Hiệu ứng âm thanh
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip enemyDefeatSound;
    [SerializeField] private AudioClip trampolineSound;

    [SerializeField] private float musicVolume = 0.7f;
    [SerializeField] private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        }
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện khi scene thay đổi
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Gọi hàm cập nhật nhạc nền mỗi khi scene mới được tải
        PlayBackgroundMusicBasedOnScene();
    }

    private void PlayBackgroundMusicBasedOnScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Menu":
                PlayBackgroundMusic(menuMusic);
                break;
            case "Level 1":
                PlayBackgroundMusic(level1Music);
                break;
            case "Level 2":
                PlayBackgroundMusic(level2Music);
                break;
            case "Level 3":
                PlayBackgroundMusic(level3Music);
                break;
            default:
                PlayBackgroundMusic(menuMusic);
                break;
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (clip != null && (backgroundMusicSource.clip != clip || !backgroundMusicSource.isPlaying))
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.Play();
        }
    }

    public void PlayCollectSound()
    {
        if (collectSound != null)
        {
            sfxSource.PlayOneShot(collectSound);
        }
    }

    public void PlayEnemyDefeatSound()
    {
        if (enemyDefeatSound != null)
        {
            sfxSource.PlayOneShot(enemyDefeatSound);
        }
    }

    public void PlayTrampolineSound()
    {
        if (trampolineSound != null)
        {
            sfxSource.PlayOneShot(trampolineSound);
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        backgroundMusicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }
}