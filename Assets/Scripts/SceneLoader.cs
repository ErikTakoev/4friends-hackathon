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
    
    void Start()
    {
        if (introShown)
        {
            ShowButton();
            return;
        }

        if (button != null)
        {
            button.gameObject.SetActive(false);
        }

        if (player == null || rawImage == null)
        {
            Debug.Assert(false);
            ShowButton();
            return;
        }

        player.url = Application.streamingAssetsPath + "/Video/Intro.mp4";
        player.prepareCompleted += OnPrepared;
        player.errorReceived += OnVideoError;
        player.loopPointReached += OnVideoEnd;
        player.Prepare();
    }

    private void OnPrepared(VideoPlayer player)
    {
        if (firstFrame != null)
        {
            firstFrame.gameObject.SetActive(false);
        }

        player.Play();
        rawImage.texture = player.texture;
    }

    private void OnVideoError(VideoPlayer player, string error)
    {
        Debug.LogError("Cannot play Intro due to error '" + error + "'!");
        ShowButton();
    }

    private void OnVideoEnd(VideoPlayer player)
    {
        Debug.Log("Intro finished.");
        introShown = true;
        ShowButton();
    }

    private void ShowButton()
    {
        if (firstFrame != null)
        {
            firstFrame.gameObject.SetActive(false);
        }

        if (player != null)
        {
            player.prepareCompleted -= OnPrepared;
            player.errorReceived -= OnVideoError;
            player.loopPointReached -= OnVideoEnd;
            player.gameObject.SetActive(false);
        }

        if (button == null)
        {
            LoadScene();
        }
        else
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(LoadScene);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
