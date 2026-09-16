using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    //guide
    //https://www.youtube.com/watch?v=J1ng1zA3-Pk
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
        obj.name = "Linear Progress Bar";
    }
    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
        obj.name = "Radial Progress Bar";
    }

#endif

    public Image Mask;
    public Image Fill;
    private Image usedToFill;
    public float current;
    public float maximum;
    public float minimum;
    public bool radialMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Mask.fillMethod == Image.FillMethod.Radial360)
        {
            Mask.gameObject.SetActive(radialMask);
            usedToFill = Fill;
        }
        else
        {
            usedToFill = Mask;
        }
    }

    public void getCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        usedToFill.fillAmount = Mathf.Clamp01(currentOffset / maximumOffset);
    }
}
