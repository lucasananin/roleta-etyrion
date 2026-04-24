using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    [SerializeField] RouletteWheel _wheel = null;
    [Space]
    [SerializeField] SlotBehaviour _prefab = null;
    [SerializeField] RectTransform _parent = null;
    [Space]
    [SerializeField] GameObject _iconPrefab = null;
    [SerializeField] RectTransform _iconParent = null;
    [Space]
    [SerializeField] Color _initialColor = Color.white;
    [SerializeField] List<Color> _colorList = null;

    private void Start()
    {
        int _count = _wheel.NumberOfSlots;

        for (int i = 0; i < _count; i++)
        {
            var _slotAngle = _wheel.GetSlotAngle();
            var _rotation = Quaternion.Euler((i + 1) * _slotAngle * Vector3.forward);
            var _slot = Instantiate(_prefab, _parent.position, _rotation, _parent);

            var _index = (int)Mathf.Repeat(i, _colorList.Count);
            var _color = i == 0 ? _initialColor : _colorList[_index];
            _slot.Init(_wheel.NumberOfSlots, _color);

            _rotation *= Quaternion.Euler(0, 0, -_wheel.GetSlotAngle() / 2f);
            var _icon = Instantiate(_iconPrefab, _iconParent.position, _rotation, _iconParent);
            _icon.GetComponentInChildren<TextMeshProUGUI>().text = $"{i}";
        }
    }
}
