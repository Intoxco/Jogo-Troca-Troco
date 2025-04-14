using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
   [SerializeField] string videoFileName;
    void Start()
    {
        PlayVideo();
    }

    // Update is called once per frame
    public void PlayVideo(){
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if(videoPlayer){
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath,videoFileName);
            videoPlayer.url=videoPath;
        }
    }
}
