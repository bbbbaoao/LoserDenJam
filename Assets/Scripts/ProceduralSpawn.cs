using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnList = new List<GameObject>();
    //use this to spread each level
    [SerializeField] private Vector2 _spawnOffset;
    //use this to determine when to trigger, must be bigger than the prefab's size
    [SerializeField] private Vector2 _interactOffset;
    //spawn count is the list count
    [SerializeField] private int _spawnCount;
    [SerializeField] private GameObject _player;
    private GameObject[,] _levels;
    private List<GameObject> _horizontalLevels = new List<GameObject>();
    private List<GameObject> _verticalLevels = new List<GameObject>();
    private int _currentHorLevel;
    private int _currentVertLevel;
    private void Awake()
    {
        _levels = new GameObject[_spawnCount * 2 + 1, _spawnCount * 2 + 1];
        for (int i = 0; i < _spawnCount * 2 + 1; i ++)
        {
            for(int j = 0; j < _spawnCount * 2 + 1; j++)
            {
                _levels[i, j] = GetRandomLevel(_player.transform.position + new Vector3(((i - _spawnCount) * _spawnOffset.x), 0, ((j- _spawnCount) * _spawnOffset.y)));
            }
            //_horizontalLevels.Add(GetRandomLevel(_player.transform.position + new Vector3 ((i *_spawnOffset.x), 0, 0)));
            //_verticalLevels.Add(GetRandomLevel(_player.transform.position + new Vector3(0, 0,(i * _spawnOffset.y))));
        }
        _currentHorLevel = _spawnCount;
        _currentVertLevel = _spawnCount;
        //_currentHorLevel = _horizontalLevels[_spawnCount];
        //_currentVertLevel = _verticalLevels[_spawnCount];
    }
    public void SpawnLevel()
    {

    }

    public GameObject GetRandomLevel(Vector3 pos)
    {
        int rand = Random.Range(0, _spawnList.Count);
        GameObject g = Instantiate(_spawnList[rand], pos, Quaternion.identity);
        return g;
    }

    private void FixedUpdate()
    {
        GameObject g = _levels[_currentHorLevel, _currentVertLevel];
        float horDist = _player.transform.position.x - g.transform.position.x;
        if (Mathf.Abs(horDist) >= _interactOffset.x)
        {
            
            int newObjIndex = _currentHorLevel - _spawnCount * (int)Mathf.Sign(horDist);
            if (newObjIndex < 0) newObjIndex += _spawnCount * 2 + 1;
            if (newObjIndex > _spawnCount * 2) newObjIndex -= _spawnCount * 2 + 1;

            for (int i = 0; i < _spawnCount * 2 + 1; i++)
            {
                GameObject objectToMove = _levels[newObjIndex, i];
                Vector3 newPos = Vector3.zero;
                newPos.x += _spawnOffset.x * _spawnCount * 2 * Mathf.Sign(horDist);
                objectToMove.transform.position += newPos;
            }

            int newhorIndex = _currentHorLevel + (int)Mathf.Sign(horDist);
            if (newhorIndex< 0 ) newhorIndex += _spawnCount * 2 + 1;
            if (newhorIndex > _spawnCount * 2) newhorIndex -= _spawnCount * 2 + 1;
            _currentHorLevel = newhorIndex;
        }
        float vertDist = _player.transform.position.z - g.transform.position.z;
        if(Mathf.Abs(vertDist) >= _interactOffset.y)
        {
            int newObjIndex = _currentVertLevel - _spawnCount * (int)Mathf.Sign(vertDist);
            if (newObjIndex < 0) newObjIndex += _spawnCount * 2 + 1;
            if (newObjIndex > _spawnCount * 2) newObjIndex -= _spawnCount * 2 + 1;
            Debug.Log(newObjIndex);
            for (int i = 0; i < _spawnCount * 2 + 1; i++)
            {
                GameObject objectToMove = _levels[i, newObjIndex];
                Vector3 newPos = Vector3.zero;
                newPos.z += _spawnOffset.y * _spawnCount * 2 * Mathf.Sign(vertDist);
                objectToMove.transform.position += newPos;
            }

            int newvertIndex = _currentVertLevel + (int)Mathf.Sign(vertDist);
            if (newvertIndex < 0) newvertIndex += _spawnCount * 2 + 1;
            if (newvertIndex > _spawnCount * 2) newvertIndex -= _spawnCount * 2 + 1;
            _currentVertLevel = newvertIndex;
        }
    }
}
