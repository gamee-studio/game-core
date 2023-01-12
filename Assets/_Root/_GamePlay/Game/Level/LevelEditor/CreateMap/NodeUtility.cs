using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class NodeUtility
{

    public static void LinkNodes(Node a, Node b)
    {
        a.AddNeighbour(b);
        b.AddNeighbour(a);
    }

    public static void UnlinkNodes(Node a, Node b)
    {
        a.RemoveNeighbour(b);
        b.RemoveNeighbour(a);
    }





#if UNITY_EDITOR

    //[MenuItem("GameObject/Link 2 Nodes", false, 0)]
    public static void LinkNodes()
    {
        var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>()).ToList();
        nodes = nodes.Where(i => i != null).ToList();
        if (nodes.Count() == 2)
        {
            var node0 = nodes[0];
            var node1 = nodes[1];
            if (!node0.neighbours.Contains(node1))
            {
                node0.neighbours.Add(node1);
            }

            if (!node1.neighbours.Contains(node0))
            {
                node1.neighbours.Add(node0);
            }
            EditorUtility.SetDirty(node0);
            EditorUtility.SetDirty(node1);
        }
    }

    //[MenuItem("GameObject/UnLink All With Nodes", false, 0)]
    public static void UnLink()
    {
        var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>());
        nodes = nodes.Where(i => i != null);
        foreach (var item in nodes)
        {
            foreach (var nb in item.neighbours)
            {
                nb.RemoveNeighbour(item);
                EditorUtility.SetDirty(nb);
            }
            item.RemoveAllNeighbour();
            EditorUtility.SetDirty(item);
        }
    }

    //[MenuItem("GameObject/UnLink 2 Nodes Only", false, 0)]
    public static void UnlinkTwoNode()
    {
        var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>()).ToArray();
        nodes = nodes.Where(i => i != null).ToArray();
        if (nodes.Count() == 2)
        {
            var node0 = nodes[0];
            var node1 = nodes[1];
            if (node0.neighbours.Contains(node1))
            {
                node0.neighbours.Remove(node1);
            }

            if (node1.neighbours.Contains(node0))
            {
                node1.neighbours.Remove(node0);
            }
            EditorUtility.SetDirty(node0);
            EditorUtility.SetDirty(node1);
        }
    }

    //[MenuItem("GameObject/Link Follow Order", false, 0)]
    //public static void LinkFollowOrder() //TODO TNX
    //{
    //    var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>()).ToArray();
    //    foreach (var node in nodes)
    //    {
    //        //Debug.LogError(node.name);
    //    }
    //}

    //[MenuItem("GameObject/Insert New Node", false, 0)]
    //public static void InsertNode()
    //{
    //    var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>()).ToArray();
    //    if (nodes.Length == 2)
    //    {
    //        var node1 = nodes[0];
    //        var node2 = nodes[1];
    //        if (node1.HadNeighbour(node2) && node2.HadNeighbour(node1))
    //        {
    //            var newNode = new GameObject("Node", typeof(Node));
    //            newNode.transform.SetParent(node1.transform.parent);
    //            newNode.transform.position = (node1.Position + node2.Position) / 2;

    //            var no = newNode.GetComponent<Node>();
    //            LinkNodes(node1, no);
    //            LinkNodes(node2, no);

    //            node1.RemoveNeighbour(node2);
    //            node2.RemoveNeighbour(node1);
    //            EditorUtility.SetDirty(node1);
    //            EditorUtility.SetDirty(node2);
    //        }
    //    }
    //}

    //[MenuItem("GameObject/Append New Node", false, 0)]
    public static void AppendNewNode()
    {
        var nodes = Selection.gameObjects.Select(i => i.GetComponent<Node>()).ToArray();
        nodes = nodes.Where(i => i != null).ToArray();
        if (nodes.Length == 1)
        {
            var node = nodes[0];

            var newNode = new GameObject("Node", typeof(Node));
            newNode.transform.SetParent(node.transform.parent);
            newNode.transform.position = node.Position + new Vector2(Random.Range(-2, 2), 0);

            var no = newNode.GetComponent<Node>();
            LinkNodes(node, no);
            Selection.activeGameObject = newNode;
        }
    }


#endif

}
