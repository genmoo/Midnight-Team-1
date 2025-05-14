using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Result
{
    public string[] list;
}

public class Api_GetFile
{
    /// <summary>
    /// Texture2D를 지정 경로에 PNG 또는 JPG로 저장합니다.
    /// </summary>
    public static void SaveTextureToFile(Texture2D texture, string filePath, string fileFormat = "png", int jpgQuality = 95)
    {
        byte[] bytes = null;
        switch (fileFormat.ToLower())
        {
            case "jpg":
            case "jpeg":
                bytes = texture.EncodeToJPG(jpgQuality);
                filePath += ".jpg";
                break;
            case "png":
            default:
                bytes = texture.EncodeToPNG();
                filePath += ".png";
                break;
        }
        File.WriteAllBytes(filePath, bytes);
    }

    /// <summary>
    /// 이미지 리스트를 받아 Sprite로 반환하고, 동시에 지정 폴더에 저장
    /// </summary>
    public static IEnumerator GetSpritesAndSave(string saveDirectory, string fileFormat, Action<List<Sprite>> OnCompleted)
    {
        // 저장 폴더가 없으면 생성
        if (!Directory.Exists(saveDirectory))
            Directory.CreateDirectory(saveDirectory);

        var getUWR = UnityWebRequest.Get("http://192.168.0.100:8080/api/list");
        yield return getUWR.SendWebRequest();

        if (getUWR.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("파일 리스트 요청 실패: " + getUWR.error);
            OnCompleted?.Invoke(null);
            yield break;
        }

        string jsonText = getUWR.downloadHandler.text;
        Result res = JsonConvert.DeserializeObject<Result>(jsonText);

        List<Sprite> sprites = new List<Sprite>();
        foreach (var filename in res.list)
        {
            var uwr = UnityWebRequestTexture.GetTexture($"http://192.168.0.100:8080/api/download/{filename}");
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"이미지 다운로드 실패: {filename} - {uwr.error}");
                continue;
            }

            var tex = DownloadHandlerTexture.GetContent(uwr);

            // Sprite 생성
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            sprite.name = filename;
            sprites.Add(sprite);

            // 파일 저장
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filename);
            string savePath = Path.Combine(saveDirectory, fileNameWithoutExt);
            SaveTextureToFile(tex, savePath, fileFormat);
        }

        OnCompleted?.Invoke(sprites);
    }
}
