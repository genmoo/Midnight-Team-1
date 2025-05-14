using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WebSample : MonoBehaviour
{
    public Image image; // 인스펙터에서 UI Image 연결

    // 원하는 저장 경로와 포맷 지정
    public string saveDirectory = "";
    public string fileFormat = "png"; // 또는 "jpg"

     public Texture2D uploadTexture;

    public void SendUploadFile()
    {
        Debug.Log("SendUploadFile");
        StartCoroutine(Api_UploadFile.Send(uploadTexture));
    }


    public void DownloadImg()
    {
        StartCoroutine(DelayedFunction());
    }
    private IEnumerator DelayedFunction()
    {
        yield return new WaitForSeconds(5f);
        DownloadAndShowImages();
    }

    public void DownloadAndShowImages()
    {
        if (string.IsNullOrEmpty(saveDirectory))
            saveDirectory = Application.dataPath + "/DownloadedImages";

        StartCoroutine(Api_GetFile.GetSpritesAndSave(saveDirectory, fileFormat, (sprites) =>
        {
            if (sprites != null && sprites.Count > 0)
            {
                image.sprite = sprites[0];
                Debug.Log("이미지 다운로드 및 저장 완료!");
            }
            else
            {
                Debug.Log("이미지 다운로드 실패!");
            }
        }));
    }
}
