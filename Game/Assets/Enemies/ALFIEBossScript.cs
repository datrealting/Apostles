using UnityEngine;

public class ALFIEBossScript : MonoBehaviour
{
    public bool active; // is the boss active or not
    public enum Phase { Phase1, Phase2 };
    public Phase phase;
    public GameObject target;

    // Some stats
    public float maxSpeed = 3f;
    public float speed = 3f;

    void Awake()
    {
        target = GameObject.Find("Player");
    }

    void Activate()
    {
        active = true;
        phase = Phase.Phase1;  
    }
    
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Activate();
        }
        if (!active)
        {
            //
        }
        else
        {
            switch (phase)
            {
                case Phase.Phase1:
                    RunPhase1();
                    break;
                case Phase.Phase2:
                    RunPhase2();
                    break;
            }
        }
    }
    void RunPhase1()
    {
        MoveToPlayer();
        Debug.Log(GetDistanceToPlayer());
    }
    void RunPhase2()
    {

    }
    void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    float GetDistanceToPlayer()
    {
        return Vector2.Distance(target.transform.position, transform.position);
    }
}

