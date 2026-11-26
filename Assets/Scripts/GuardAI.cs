using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GuardAI : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public TextMeshProUGUI statusText;

    public float detectRange = 11f;
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5.2f;

    private NavMeshAgent agent;
    private LineRenderer line;
    private int currentIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        string currentState;

        if (distance <= detectRange)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            currentState = "PERSIGUIENDO";
        }
        else
        {
            agent.speed = patrolSpeed;
            Patrol();
            currentState = "PATRULLANDO";
        }

        DrawPath();
        UpdateText(currentState);
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }

    void DrawPath()
    {
        if (line == null || agent.path == null) return;

        Vector3[] corners = agent.path.corners;
        line.positionCount = corners.Length;

        for (int i = 0; i < corners.Length; i++)
        {
            line.SetPosition(i, corners[i] + Vector3.up * 0.15f);
        }
    }

    void UpdateText(string state)
    {
        if (statusText == null) return;

        float dist = agent.pathPending ? 0f : agent.remainingDistance;
        statusText.text = $"Estado IA: {state}\nDistancia: {dist:F1} m\n[Espacio] Bloquear pasillo";
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}