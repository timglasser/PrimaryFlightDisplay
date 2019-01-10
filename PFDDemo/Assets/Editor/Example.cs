using UnityEditor;
using UnityEngine;
using UnityEditor.Graphs;

public class Example : EditorWindow
{
    static Example example;
    Graph stateMachineGraph;
    GraphGUI stateMachineGraphGUI;

    [MenuItem("Window/StateMachine")]
    static void Do()
    {
        example = GetWindow<Example>();
    }

    void OnEnable()
    {
        if (stateMachineGraph == null)
        {
            stateMachineGraph = ScriptableObject.CreateInstance<Graph>();
            stateMachineGraph.hideFlags = HideFlags.HideAndDontSave;
        }



        //create new node
        Node node1 = ScriptableObject.CreateInstance<Node>();
        node1.title = "state Firing";
        node1.position = new Rect(100, 34, 300, 200);


        Slot start = node1.AddInputSlot("input");
       // node1.AddInputSlot("input");
        node1.AddOutputSlot("output");
        node1.AddProperty(new Property(typeof(System.Int32), "temperature"));
        stateMachineGraph.AddNode(node1);

        //create new node
        Node node2 = ScriptableObject.CreateInstance<Node>();
        node2.title = "state NOT Firing";
        node2.position = new Rect(0, 0, 300, 200); 
        
    
        Slot end = node2.AddInputSlot("input");
        node2.AddOutputSlot("output");
        node2.AddProperty(new Property(typeof(System.Int32), "bullets"));
        stateMachineGraph.AddNode(node2);

        //create connection
        stateMachineGraph.Connect(start, end);

        // GUI
        stateMachineGraphGUI = ScriptableObject.CreateInstance<GraphGUI>();
        stateMachineGraphGUI.graph = stateMachineGraph;
        stateMachineGraphGUI.hideFlags = HideFlags.HideAndDontSave;


    }

    void OnDisable()
    {
        example = null;
    }

    void OnGUI()
    {
        if (example && stateMachineGraphGUI != null)
        {
            stateMachineGraphGUI.BeginGraphGUI(example, new Rect(0, 0, example.position.width, example.position.height));
                       stateMachineGraphGUI.OnGraphGUI ();
            stateMachineGraphGUI.EndGraphGUI();

        }
    }

}

