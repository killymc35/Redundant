using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region Variables
    public List<Behaviour> CurrentBehaviours = new List<Behaviour>();
    
    public List<Behaviour> BehavioursToRemove = new List<Behaviour>();
    public List<Behaviour> BehavioursToAdd = new List<Behaviour>();
    
    public static PlayerBehaviour Instance;
    
    public CapsuleCollider playerCollider;
    public LayerMask collisionLayers;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    
    public GameObject CameraHolder;
    #endregion
    
    #region Private Methods
    private void RunAll(List<Behaviour> behaviours)
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.Run();
        }
    }
    private void ClearOldBehaviours()
    {
        foreach (Behaviour behaviour in BehavioursToRemove)
        {
            behaviour.End();
            CurrentBehaviours.Remove(behaviour);
        }
        BehavioursToRemove.Clear();
    }
    private void AddNewBehaviours()
    {
        foreach (Behaviour behaviour in BehavioursToAdd)
        {
            behaviour.Begin();
            CurrentBehaviours.Add(behaviour);
        }
        BehavioursToAdd.Clear();
    }
    #endregion
    #region Public Methods
    public void Begin<T>() where T : Behaviour, new()
    {
        if (InState<T>()) return;

        var newBehaviour = new T();
        
        BehavioursToAdd.Add(newBehaviour);
    }
    public void End<T>() where T : Behaviour
    {
        foreach (Behaviour behaviour in CurrentBehaviours)
        {
            if (behaviour is T)
            {
                BehavioursToRemove.Add(behaviour);
            }
        }
    }
    public bool InState<T>() where T : Behaviour
    {
        foreach (Behaviour behaviour in CurrentBehaviours)
        {
            if (behaviour is T) return true;
        }
        return false;
    }
    #endregion
    private void Start()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        RunAll(CurrentBehaviours);
        ClearOldBehaviours();
        AddNewBehaviours();

        #region Update Positions & Rotations

        playerPosition = transform.position;
        playerRotation = transform.rotation;

        #endregion
    }
}
