using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public bool pingPong;

    public float emissionIntensityUncharged = 0.0f;
    public float emissionIntensityCharged = 10.0f;

    public bool charged = true;

    Color currentColor = Color.black;
    public Color unchargedColor;
    public float maxClosestDistance;
    public Color closestColor;
    public float maxMiddleDistance;
    public Color middleColor;
    public float maxFarthestDistance;
    public Color farthestColor;
    public Color deathColor;

    public GameObject player1, player2;
    Rigidbody p1rb, p2rb;

    float playerDistance;

    public GameObject nodeAsset;
    public string nodeName = "Node";
    public string nodeDeleteAppendage = "[d]";
    public string nodeAddAppendage = "[a]";
    public float nodeBuffer = 0.01f;

    int numNodes;
    List<GameObject> nodes;
    List<Material> materials;
    Stack<GameObject> deletedNodes;

    List<Joint> jointsBackward;
    List<Joint> jointsForward;

    List<Rigidbody> rigidbodies;

    public enum JointType { HingeJoint, SpringJoint, CharacterJoint };
    public JointType jointType;

    static readonly Vector3 connectedAnchor = new Vector3(-5.960462e-08f, -0.9999939f, 0f);

	// Use this for initialization
	void Start () {
        nodes = new List<GameObject>();
        materials = new List<Material>();
        deletedNodes = new Stack<GameObject>();

        jointsBackward = new List<Joint>();
        jointsForward = new List<Joint>();

        rigidbodies = new List<Rigidbody>();

        p1rb = player1.GetComponent<Rigidbody>();
        p2rb = player2.GetComponent<Rigidbody>();

        playerDistance = Vector3.Distance(player1.transform.position, player2.transform.position);

        CreateNodes();
        AttachToPlayers();

        EvaluateColor();
        PaintColor();
	}
	
	// Update is called once per frame
	void Update () {
        EvaluateColor();
        PaintColor();
	}

    void PaintColor()
    {
        foreach (Material m in materials)
        {
            m.color = currentColor;

            float emission = charged ? emissionIntensityCharged : emissionIntensityUncharged;

            if (pingPong)
                emission = Mathf.PingPong(Time.time, emission);

            Color emissionColor = currentColor * Mathf.LinearToGammaSpace(emission);
            m.SetColor("_EmissionColor", emissionColor);
        }
    }

    void EvaluateColor()
    {
		if (playerDistance <= maxClosestDistance) {
			currentColor = closestColor;
			GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Close;
		} else if (playerDistance <= maxMiddleDistance) {
			currentColor = middleColor;
			GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Medium;
		} else if (playerDistance <= maxFarthestDistance) {
			currentColor = farthestColor;
			GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Far;
		}else
            currentColor = deathColor;
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Delete middle");
            RemoveNodeAtMiddle();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddNodeToMiddle();
        }


        //UpdateRopeYPosition();
    }

    void SetAutoConfigureConnectedAnchor(int i, bool val)
    {
        jointsBackward[i - 1].autoConfigureConnectedAnchor = val;
        jointsForward[ + 1].autoConfigureConnectedAnchor = val;
    }

    void RemoveNodeAtMiddle()
    {
        int m = numNodes / 2;
        var node = nodes[m];

        Debug.Assert(nodes.Count() == jointsForward.Count(), "nodes.Count() == jointsForward.Count()");
        Debug.Assert(nodes.Count() == jointsBackward.Count(), "nodes.Count() == jointsBackward.Count()");
        Debug.Assert(nodes.Count() == rigidbodies.Count(), "nodes.Count() == rigidbodies.Count()");

        SetAutoConfigureConnectedAnchor(m, false);

        jointsBackward[m].connectedBody = rigidbodies[m - 1];
        jointsForward[m].connectedBody = rigidbodies[m + 1];

        nodes.Remove(node);
        jointsBackward.Remove(jointsBackward[m - 1]);
        jointsForward.Remove(jointsForward[m + 1]);
        rigidbodies.Remove(rigidbodies[m]);

        node.SetActive(false);

        deletedNodes.Push(node);
        --numNodes;
    }

    void AddNodeToMiddle()
    {
        int m = numNodes / 2;

        if (!deletedNodes.Any())
        {
            Debug.LogError("Empty deletedNodes list, but adding new node");
            return;
        }

        var node = deletedNodes.Pop();

        nodes.Insert(m, node);
        rigidbodies.Insert(m, node.GetComponent<Rigidbody>());


        Debug.Log(rigidbodies[m].gameObject.name);

        HingeJoint[] joints = node.GetComponents<HingeJoint>();
        joints[0].connectedBody = rigidbodies[m - 1];
        joints[1].connectedBody = rigidbodies[m + 1];
        jointsBackward.Insert(m - 1, joints[0]);
        jointsForward.Insert(m + 1, joints[1]);

        jointsBackward[m].connectedBody = rigidbodies[m];
        jointsForward[m].connectedBody = rigidbodies[m];

        var midPt = Vector3.Lerp(rigidbodies[m - 1].position, rigidbodies[m - 1].position, 0.5f);
        midPt += GetDirectionalIncrement(node);
        Debug.Log(string.Format("{0}", midPt));

        rigidbodies[m].position = midPt;
        nodes[m].transform.position = midPt;
        rigidbodies[m].velocity = Vector3.zero;
        rigidbodies[m].angularVelocity = Vector3.zero;

        SetAutoConfigureConnectedAnchor(m, true);
        //node.name = nodeName + nodeAddAppendage;
        node.SetActive(true);

        Debug.Break();

        ++numNodes;
    }

    void CreateNodes() {
        var pos = transform.position;
        var directionalIncrement = GetDirectionalIncrement(nodeAsset);

        var p1col = player1.GetComponent<Collider>();
        var p2col = player2.GetComponent<Collider>();

        Debug.Assert(p1col != null);
        Debug.Assert(p2col != null);

        numNodes = Mathf.CeilToInt(playerDistance / directionalIncrement.magnitude);

        for (int i = 0; i < numNodes; ++i) 
        {
            GameObject n = Instantiate(nodeAsset, pos, nodeAsset.transform.rotation);
            n.name = nodeName + " (" + i.ToString() + ")";
            n.tag = "Chain";
            n.transform.parent = transform;

            Physics.IgnoreCollision(n.GetComponent<Collider>(), p1col);
            Physics.IgnoreCollision(n.GetComponent<Collider>(), p2col);

            nodes.Add(n);
            materials.Add(n.GetComponent<Renderer>().material);

            var rb = n.GetComponent<Rigidbody>();
            rigidbodies.Add(rb);
            rb.isKinematic = true;

            pos += directionalIncrement;
        }

        var colls = from n in nodes select n.GetComponent<Collider>();
        foreach (var c1 in colls)
            foreach (var c2 in colls)
                if (c1 != c2)
                    Physics.IgnoreCollision(c1, c2);

        foreach (var n in nodes)
        {
            Joint j1 = null, j2 = null;

            switch (jointType) {
                case JointType.HingeJoint:
                    j1 = n.AddComponent(typeof(HingeJoint)) as Joint;
                    j2 = n.AddComponent(typeof(HingeJoint)) as Joint;
                    ConfigureHingeJoints(j1);
                    break;
                case JointType.SpringJoint:
                    j1 = n.AddComponent(typeof(SpringJoint)) as Joint;
                    j2 = n.AddComponent(typeof(SpringJoint)) as Joint;
                    ConfigureSpringJoints(j1);
                    break;
                case JointType.CharacterJoint:
                    j1 = n.AddComponent(typeof(CharacterJoint)) as Joint;
                    j2 = n.AddComponent(typeof(CharacterJoint)) as Joint;
                    ConfigureCharacterJoints(j1);
                    break;
            }

            j1.enablePreprocessing = false;
            j2.enablePreprocessing = false;

            j1.axis = new Vector3(0.0f, 1.0f, 0.0f);
            j2.axis = new Vector3(0.0f, -1.0f, 0.0f);

            jointsBackward.Add(j1);
            jointsForward.Add(j2);
        }

        Debug.Assert(nodes.Count() == jointsBackward.Count());

        for (int i = 1; i < nodes.Count(); ++i)
        {
            jointsBackward[i].connectedBody = rigidbodies[i - 1];
            jointsForward[i - 1].connectedBody = rigidbodies[i];
        }
    }

    Vector3 GetDirectionalIncrement(GameObject node)
    {
        var directionalIncrement = node.transform.rotation * node.transform.localScale;
        directionalIncrement = Vector3.Project(directionalIncrement, -node.transform.up) * 2.0f;
        return directionalIncrement;
    }

    void ConfigureHingeJoints(params Joint[] joints)
    {
        var hingeJoints = from j in joints select j as HingeJoint;
        foreach (HingeJoint j in hingeJoints)
        {
            j.useSpring = true;
            var s = j.spring;
            s.damper = 500.0f;
            s.spring = 1000.0f;
            j.spring = s;

            j.connectedAnchor = connectedAnchor;
        }
    }

    void ConfigureSpringJoints(params Joint[] joints)
    {
        var springJoints = from j in joints select j as SpringJoint;
        foreach (SpringJoint j in springJoints)
        {
            //j.spring = 100.0f;
            j.damper = 100.0f;
        }
    }

    void ConfigureCharacterJoints(params Joint[] joints)
    {
        var charJoints = from j in joints select j as CharacterJoint;
        foreach (CharacterJoint j in charJoints)
        {
            var sl = j.swingLimitSpring;
            sl.spring = 1000.0f;
            sl.damper = 50.0f;
            j.swingLimitSpring = sl;
            var tl = j.twistLimitSpring;
            tl.damper = 50.0f;
            j.twistLimitSpring = tl;
        }
    }

    void AttachToPlayers()
    {
        var j1 = jointsBackward.First();
        var j2 = jointsForward.Last();

        Debug.Assert(j1.connectedBody == null);
        Debug.Assert(j2.connectedBody == null);

        jointsBackward.Remove(j1);
        jointsForward.Remove(j2);

        Destroy(j1);
        Destroy(j2);

        j1 = nodes.First().AddComponent(typeof(SpringJoint)) as Joint;
        j2 = nodes.Last().AddComponent(typeof(SpringJoint)) as Joint;

        ((SpringJoint)j1).spring = 1000.0f;
        ((SpringJoint)j2).spring = 1000.0f;

        jointsForward.Insert(0, j1);
        jointsBackward.Add(j2);

        transform.position = player2.transform.position;

        j2.connectedBody = player1.GetComponent<Rigidbody>();
        j1.connectedBody = player2.GetComponent<Rigidbody>();

        foreach (var rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        SetKinematic(false);
    }

    void SetKinematic(bool isKinematic)
    {
        foreach (var rb in rigidbodies)
            rb.isKinematic = isKinematic;
    }

    void UpdateRopeYPosition()
    {
        var y = p1rb.position.y;
        var step = (p2rb.position.y - p1rb.position.y) / (float) numNodes;


        foreach (var rb in rigidbodies)
            rb.constraints = RigidbodyConstraints.None;
        foreach (var rb in rigidbodies)
        {
            rb.position = new Vector3(rb.position.x, y, rb.position.z);
            y += step;
        }
        foreach (var rb in rigidbodies)
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    public void IgnoreCollisionsWith(Collider collider)
    {
        foreach (var n in nodes)
            Physics.IgnoreCollision(n.GetComponent<Collider>(), collider);
    }
}
