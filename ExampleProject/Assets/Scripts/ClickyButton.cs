using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _unCompressClip;
    [SerializeField] private AudioSource _source;

    public void OnPointerDown(PointerEventData eventData)
    {
        _text.margin = new Vector4(21f, 30f, 21f, 14f);
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _text.margin = new Vector4(21f, 7f, 21f, 37f);
        _img.sprite = _default;
        _source.PlayOneShot(_unCompressClip);
    }

    public void OnClickButton()
    {
    }
}