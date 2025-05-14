// public class WebSample : MonoBehaviour
// {
//     public UnityEngine.UI.Image image;

//     public void DownloadAndShowImages()
//     {
//         string saveDir = Application.dataPath + "/DownloadedImages";
//         string fileFormat = "png";
//         StartCoroutine(Api_GetFile.GetSpritesAndSave(saveDir, fileFormat, (sprites) =>
//         {
//             if (sprites != null && sprites.Count > 0)
//                 image.sprite = sprites[0];
//         }));
//     }
// }
