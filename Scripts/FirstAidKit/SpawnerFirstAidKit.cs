using UnityEngine;

public class SpawnerFirstAidKit : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private FirstAidKit _firstAidKit;

    private Transform[] _places;

    private void Awake()
    {
        _places = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _places.Length; i++)
        {
            _places[i] = _spawnPoints.GetChild(i);
        }
    }

    private void Start()
    {
        for (int i = 0; i < _places.Length; i++)
        {
            Instantiate(_firstAidKit, _places[i].position, Quaternion.identity);
        }
    }
}