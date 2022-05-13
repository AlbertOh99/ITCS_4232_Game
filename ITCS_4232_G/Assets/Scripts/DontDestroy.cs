using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy mus = null;
    public static DontDestroy Mus
    {
        get { return mus; }
    }


    private void Awake()
    {
        if (mus != null && mus != this)
        {
            Destroy(this.gameObject);
        } else
        {
            mus = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
