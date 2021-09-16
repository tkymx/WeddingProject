using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugParameter
{
    public bool DiceForce = false;
    public int DiceForceCount = 0;
    public bool AllZukan = false;

    public DebugParameter()
    {
    }
}

public class DebugMenu : MonoBehaviour
{
    [SerializeField]
    GameObject debugMenu = null;

    [SerializeField]
    GameObject contents = null;

    [SerializeField]
    Button OpenButton = null;

    [SerializeField]
    Button CloseButton = null;

    public DebugParameter DebugParameter = new DebugParameter();

    // Use this for initialization
    void Start()
    {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
        OpenButton.gameObject.SetActive(false);
#endif

        debugMenu.SetActive(false);

        OpenButton.onClick.AddListener(() =>
        {
            debugMenu.SetActive(true);
        });

        CloseButton.onClick.AddListener(() =>
        {
            debugMenu.SetActive(false);
        });

        DebugCell.CreateDebugCell(() =>
        {
            DebugParameter.DiceForce = !DebugParameter.DiceForce;
        }, "サイコロの移動数を固定にする", contents);

        DebugCell.CreateDebugCell(() =>
        {
            DebugParameter.DiceForceCount = 1;
        }, "サイコロの移動数を1にする", contents);

        DebugCell.CreateDebugCell(() =>
        {
            DebugParameter.DiceForceCount = 100;
        }, "サイコロの移動数を100にする", contents);

        DebugCell.CreateDebugCell(() =>
        {
            DebugParameter.AllZukan = !DebugParameter.AllZukan;
        }, "図鑑前開け", contents);
    }

    private static DebugMenu instance;
    public static DebugMenu Instance
    {
        get
        {
            if (instance == null)
            {

                instance = (DebugMenu)FindObjectOfType(typeof(DebugMenu));
                if (instance == null)
                {

                    instance = Instantiate(ResourceManager.GetPrefab("UI/DebugMenu").GetComponent<DebugMenu>());
                }
            }
            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        DontDestroyOnLoad(DebugMenu.Instance);
    }
}
