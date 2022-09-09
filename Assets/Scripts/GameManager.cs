using System;
using Database;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            // C# singleton:
            //if (m_cInstance == null) m_cInstance = new GameManager();
            
            // Unity singleton:
            if (_instance == null) _instance = FindObjectOfType<GameManager>();
            
            return _instance;
        }
    }

    private DbHandler _dbHandler;
    private DbPlayer _player;

    public DbHandler DbHandler
    {
        get
        {
            return _dbHandler;
        }
        private set
        {
            _dbHandler = value;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
            Initialize();
        }
    }

    private void OnDestroy()
    {
        DbHandler.Dispose();
    }

    private void Initialize()
    {
        DbHandler = new DbHandler();
        _player = null;
    }

    public void SetPlayer(DbPlayer player) => _player = player;

    public void ClearPlayer() => SetPlayer(null);
}
