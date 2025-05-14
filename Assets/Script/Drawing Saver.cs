using UnityEngine;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(Button))]
public class DrawingSaver : MonoBehaviour
{
    public RenderTexture renderTexture;
    public WebSample webSample; // WebSample 스크립트 연결

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(SaveTexture);
        }
    }

    public void SaveTexture()
    {
        if (renderTexture == null)
        {
            Debug.LogError("RenderTexture가 연결되지 않았습니다!");
            return;
        }

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        RenderTexture.active = currentRT;

        // 저장 경로 설정
        string folderPath = Application.dataPath + "/SaveDrawing";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string path = folderPath + $"/Saved_{timestamp}.png";

        File.WriteAllBytes(path, tex.EncodeToPNG());
        Debug.Log("그림 저장 완료: " + path);

        // ✅ WebSample에 Texture2D 넘기기
        if (webSample != null)
        {
            webSample.uploadTexture = tex;
            Debug.Log("WebSample에 uploadTexture 설정됨");
        }
        else
        {
            Debug.LogWarning("WebSample이 연결되어 있지 않습니다.");
        }
    }
}