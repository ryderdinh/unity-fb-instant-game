using System;
using System.Collections;
using BZ_FIG_SDK.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ryder : Singleton<Ryder>
{
    private RenderTexture _renderTexture;
    private Texture2D _screenShot;

    public void CheckUnityEditor(Action nonEditorCallback, Action editorCallback = null)
    {
#if !UNITY_EDITOR
               nonEditorCallback();
#endif

#if UNITY_EDITOR
        editorCallback?.Invoke();
#endif
    }

    public static string CheckUnityEditorAndReturnString(string nonEditorValue = "", string editorValue = "")
    {
#if !UNITY_EDITOR
               return nonEditorValue;
#endif

        return editorValue;
    }

    public string GenerateId()
    {
        // Generate a new Guid
        var newGuid = Guid.NewGuid();
        // Convert the Guid to a string
        var guidString = newGuid.ToString();
        return guidString;
    }

    public IEnumerator LoadImage(string url, Image image, Action onSuccess, Action onFailed)
    {
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            var texture = DownloadHandlerTexture.GetContent(www);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            onSuccess?.Invoke();
        }
        else
        {
            onFailed?.Invoke();
            Debug.Log("Cannot download image from url!");
        }
    }

    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime;
    }

    public void CaptureFullScreen(Action<string, Texture2D> callback = null, bool copyToClipboard = false)
    {
        StartCoroutine(Capture(callback, copyToClipboard));
    }

    private IEnumerator Capture(Action<string, Texture2D> callback = null, bool copyToClipboard = false)
    {
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        if (Camera.main != null)
        {
            Camera.main.targetTexture = _renderTexture;
            Camera.main.Render();

            yield return new WaitForEndOfFrame();

            _screenShot = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
            RenderTexture.active = _renderTexture;
            _screenShot.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
            _screenShot.Apply();

            const string format = "png";
            var imageBytes = _screenShot.EncodeToPNG();
            var base64String = Convert.ToBase64String(imageBytes);
            var base64ImageString = "data:image/" + format + ";base64," + base64String;

            RenderTexture.active = null;
            Camera.main.targetTexture = null;

            callback?.Invoke(base64ImageString, _screenShot);
            if (copyToClipboard)
                CopyTextToClipboard(base64ImageString);
        }

        Destroy(_renderTexture);
    }

    public void CopyTextToClipboard(string textToCopy)
    {
        GUIUtility.systemCopyBuffer = textToCopy;
    }
}