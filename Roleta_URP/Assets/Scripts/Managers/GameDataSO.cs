using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Scriptable Objects/GameDataSO")]
public class GameDataSO : ScriptableObject
{
    [SerializeField] int _numberOfSlots = 8;
    [SerializeField] Vector2 _durationRange = new(15f, 20f);
    [SerializeField] Vector2 _speedRange = new(4f, 6f);

    public int NumberOfSlots { get => _numberOfSlots; set => _numberOfSlots = value; }
    public Vector2 DurationRange { get => _durationRange; set => _durationRange = value; }
    public Vector2 SpeedRange { get => _speedRange; set => _speedRange = value; }
}
