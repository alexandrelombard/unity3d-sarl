using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodiesRepository : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultBody;
    public GameObject DefaultBody
    {
        get;
        set;
    }

    private Dictionary<string, GameObject> bodies = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Bodies
    {
        get
        {
            return bodies;
        }
        private set
        {
            this.bodies = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns a body.
    /// </summary>
    /// <returns>The spawned body</returns>
    public GameObject Spawn(string id)
    {
        // TODO Allow parameters (like giving a spawner object with body selection strategy and location strategy)
        var bodyGameObject = Instantiate(defaultBody);
        bodies.Add(id, bodyGameObject);
        return bodyGameObject;
    }

    private static BodiesRepository instance = null;

    public static bool Exists()
    {
        return instance != null;
    }

    public static BodiesRepository Instance()
    {
        if (!Exists())
        {
            throw new Exception("BodiesRepository could not find the BodiesRepository object. Please ensure you have added the BodiesRepository Prefab to your scene.");
        }
        return instance;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnDestroy()
    {
        instance = null;
    }
}
