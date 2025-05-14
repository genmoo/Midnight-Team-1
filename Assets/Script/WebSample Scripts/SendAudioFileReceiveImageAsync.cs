using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AudioToImageConverterAsync : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer; // 3D 오브젝트의 Renderer
    [SerializeField] private string shaderTextureProperty = "_Img"; // 머티리얼의 텍스처 변수명
    [SerializeField] private string audioFilePath = "Assets/Resources/sample.mp3";

    // private void Start()
    // {
    //     SendAudioFileReceiveImageAsync();
    // }

    public async void SendAudioFileReceiveImageAsync()
    {
        try
        {
            Texture2D resultTexture = await RequestAudioFileToImageAsync();
            if (resultTexture != null)
            {
                ApplyTextureToMaterial(resultTexture);
                Debug.Log("이미지 변환 및 머티리얼 적용 완료");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"오류 발생: {ex.Message}");
        }
    }
    
    private async UniTask<Texture2D> RequestAudioFileToImageAsync()
    {
        if (!File.Exists(audioFilePath))
        {
            throw new FileNotFoundException($"파일을 찾을 수 없습니다: {audioFilePath}");
        }
        
        byte[] audioData;
        try
        {
            audioData = await File.ReadAllBytesAsync(audioFilePath);
            Debug.Log($"오디오 파일 로드 완료: {audioData.Length} 바이트");
        }
        catch (Exception e)
        {
            throw new Exception($"파일 읽기 오류: {e.Message}");
        }
        
        string fileName = Path.GetFileName(audioFilePath);
        string mimeType = "audio/wav"; 
        if (!string.IsNullOrEmpty(audioFilePath))
        {
            if (audioFilePath.EndsWith(".mp3"))
                mimeType = "audio/mpeg";
            else if (audioFilePath.EndsWith(".ogg"))
                mimeType = "audio/ogg";
        }
        
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("file", audioData, fileName, mimeType));
        
        using (UnityWebRequest request = UnityWebRequest.Post("http://172.16.16.150:8000/generate-image/voice", formData))
        {
            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(request.error);
            }
            
            byte[] imageBytes = request.downloadHandler.data;
            
            Texture2D texture = new Texture2D(2, 2);
            bool isLoaded = texture.LoadImage(imageBytes);
            
            if (isLoaded)
            {
                texture.Apply();
                return texture;
            }
            else
            {
                throw new Exception("이미지 로드 실패");
            }
        }
    }

    private void ApplyTextureToMaterial(Texture2D tex)
    {
        if (targetRenderer != null && tex != null)
        {
            targetRenderer.material.SetTexture(shaderTextureProperty, tex);
        }
        else
        {
            Debug.LogWarning("Renderer 또는 Texture2D가 null입니다.");
        }
    }
}
