using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour {

    public enum Type {
        Normal,
        AbNormal,
        Event,
        Start,
        End
    }

    [SerializeField]
    private Type type = Type.Normal;

    [SerializeField]
    private Quiz quiz;

    [SerializeField]
    private ConversationTextGroup conversationTextGroup;

    [SerializeField]
    private ConversationTextGroup afterConversationTextGroup;

    public Type MasuType {
        get {
            return type;
        }
    }

    public Quiz Quiz
    {
        get {
            return quiz;
        }
        set {
            quiz = value;
        }
    }

    public ConversationTextGroup ConversationTextGroup
    {
        get
        {
            return conversationTextGroup;
        }
        set
        {
            conversationTextGroup = value;
        }
    }

    public ConversationTextGroup AfterConversationTextGroup
    {
        get
        {
            return afterConversationTextGroup;
        }
        set
        {
            afterConversationTextGroup = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsStop() {
        if (type == Type.Event || type == Type.End) {
            return true;
        }
        return false;
    }

    public bool IsFinish()
    {
        if (type == Type.End)
        {
            return true;
        }
        return false;
    }
}
