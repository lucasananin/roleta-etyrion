using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    [SerializeField] SlotBehaviour _prefab = null;
    [SerializeField] RectTransform _parent = null;
    [SerializeField] RouletteWheel _wheel = null;

    private void Start()
    {
        int _count = _wheel.NumberOfSlots;

        for (int i = 0; i < _count; i++)
        {
            var _slotAngle = _wheel.GetSlotAngle();
            var _rotation = Quaternion.Euler(Vector3.forward * _slotAngle * (i + 1));
            var _instance = Instantiate(_prefab, _parent.position, _rotation, _parent);
            _instance.RandomizeColor();
        }
    }
}
