using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Node : MonoBehaviour
{
    [SerializeField] private NodeType type;


    public List<Node> neighbours = new List<Node>();

    public bool CanSpreadHere => !HadSpread;// && ((IsDynamic && NeighbourCount >= 2) || (!IsDynamic && NeighbourCount >= 1));
    public Node[] Next() => neighbours.Where(i => i.CanSpreadHere).ToArray();

    public bool IsDynamic => type == NodeType.Dynamic;
    public bool IsRootDynamic => type == NodeType.RootDynamic;
    public bool IsStartCurveFixed => type == NodeType.StartCurveFixed;
    public bool IsEndCurveFixed => type == NodeType.EndCurveFixed;
    public Vector2 Position => transform.position;

    public int NeighbourCount => neighbours.Count;

    public bool IsFinishNode => type == NodeType.FinishNode;
    public bool IsStartNode => type == NodeType.StartNode;

    public Node NeighbourOfDynamic => neighbours[0];

    public bool HadSpread;

    public NodeType NodeType => type;

    public void AddNeighbour(Node other)
    {
        if (!neighbours.Contains(other))
        {
            neighbours.Add(other);
        }
    }

    public void RemoveNeighbour(Node other)
    {
        if (neighbours.Contains(other))
        {
            neighbours.Remove(other);
        }
    }

    public void RemoveAllNeighbour()
    {
        neighbours.Clear();
    }

    public void LinkNode(Node other)
    {
        AddNeighbour(other);
        other.AddNeighbour(this);
    }

    public void RemoveLinkWithNode(Node other)
    {
        RemoveNeighbour(other);
        other.RemoveNeighbour(this);
    }

    public bool HadNeighbour(Node other)
    {
        return neighbours.Contains(other);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < neighbours.Count; i++)
        {
            RemoveLinkWithNode(neighbours[i]);
        }
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (!Application.isPlaying && Application.isEditor)
        {
            for (int i = neighbours.Count - 1; i >= 0; i--)
            {
                if (!neighbours[i])
                {
                    neighbours.Remove(neighbours[i]);
                }
                else
                {
                    if (!neighbours[i].neighbours.Contains(this))
                    {
                        neighbours.Remove(neighbours[i]);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (neighbours.Count <= 0) return;
        Gizmos.color = Color.magenta;
        foreach (var item in neighbours)
        {
            if (item && item.neighbours.Contains(this) && neighbours.Contains(item))
            {
                Gizmos.DrawLine(transform.position, item.transform.position);
            }
        }
    }

    [ContextMenu("Log Neighbour Info")]
    private void LogNeighbour()
    {
        Debug.LogError($"Count:{NeighbourCount}");
    }

#endif
}


public enum NodeType
{
    Normal,
    Dynamic,
    RootDynamic,
    StartCurveFixed,
    EndCurveFixed,
    StartNode,
    FinishNode,
    BoilerNode
}