using UnityEngine;
using UnityEngine.UI;

public class WebSample : MonoBehaviour
{
    public Texture2D uploadTexture;

    public Image image;

    public void SendUploadFile()
    {
        Debug.Log("SendUploadFile");
        StartCoroutine(Api_UploadFile.Send(uploadTexture));
    }
    public void SendGetFile()
    {
        Debug.Log("SendGetFile");
        StartCoroutine(Api_GetFile.Send(
            Api_UploadFile.LatestUploadTextureFilename,
            sprite => image.sprite = sprite));
    }
}