using UnityEngine;

public class Falling : Behaviour
{
    #region Variables

    private float gravityCurrent;
    private const float gravityIncrease = 0.35f;
    private const float gravityMax = 40f;

    public static float StartTime = float.MaxValue;
    public static float CoyoteTime = 0.25f;
    
    #endregion
    
    public void Begin()
    {
        #region Initialize Values
        gravityCurrent = 0;
        StartTime = Time.time;
        #endregion
    }

    public void Run()
    {
        #region End State
        if (GroundCheck.Execute(PlayerBehaviour.Instance.playerCollider, PlayerBehaviour.Instance.collisionLayers) != null)
        {
            PlayerBehaviour.Instance.End<Falling>();
            return;
        }
        #endregion
        
        #region Logic
        gravityCurrent += gravityIncrease;

        gravityCurrent = Mathf.Min(gravityCurrent, gravityMax);

        var velocity = CollideAndSlide.Execute(
            PlayerBehaviour.Instance.playerCollider,
            PlayerBehaviour.Instance.collisionLayers,
            Vector3.down * (gravityCurrent * Time.deltaTime),
            PlayerBehaviour.Instance.transform.position,
            Vector3.down,
            0);
        PlayerBehaviour.Instance.transform.position += velocity;
        #endregion
    }

    public void End()
    {
        #region Reset Values
        gravityCurrent = 0;
        StartTime = float.MaxValue;
        #endregion
    }
}
