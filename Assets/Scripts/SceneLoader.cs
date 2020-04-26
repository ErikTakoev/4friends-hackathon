using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private Image firstFrame;
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;

    private static bool introShown = false;
    
    private void Start()
    {
        ShowButton();
    }

    private void PrepareVideo()
    {
        if (introShown)
        {
            LoadScene();
            return;
        }

        if (player == null || rawImage == null)
        {
            Debug.Assert(false);
            LoadScene();
            return;
        }

        if (firstFrame != null)
        {
            firstFrame.gameObject.SetActive(true);
        }

        button.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Video", "Intro.mp4");
        player.prepareCompleted += OnPrepared;
        player.errorReceived += OnVideoError;
        player.loopPointReached += OnVideoEnd;
        player.Prepare();
    }

    private void OnPrepared(VideoPlayer player)
    {
        player.Play();
        rawImage.texture = player.texture;
    }

    private void OnVideoError(VideoPlayer player, string error)
    {
        Debug.LogError("Cannot play Intro due to error '" + error + "'!");
        LoadScene();
    }

    private void OnVideoEnd(VideoPlayer player)
    {
        Debug.Log("Intro finished.");
        introShown = true;
        LoadScene();
    }

    private void ShowButton()
    {
        if (firstFrame != null)
        {
            firstFrame.gameObject.SetActive(false);
        }

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

        if (button == null)
        {
            LoadScene();
        }
        else
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(PrepareVideo);
        }
    }

    private void LoadScene()
    {
        if (player != null)
        {
            player.prepareCompleted -= OnPrepared;
            player.errorReceived -= OnVideoError;
            player.loopPointReached -= OnVideoEnd;
            player.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(sceneName);
    }
}
