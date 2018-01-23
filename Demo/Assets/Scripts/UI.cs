using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI {

    static IEnumerator printAfterTime(string s, string canvas, float t)
    {
        yield return new WaitForSeconds(t);
        GameObject sub = subtitle(s, canvas);
        GameObject.Destroy(sub, t + 3f);
    }

    public static GameObject subtitle(string s, string canvas)
    {
        Vector3 position = new Vector3(0, 60f, 0); //x, y, z
        Vector2 anchorMax = new Vector2(0.5f, 0); 
        Vector2 anchorMin = new Vector2(0.5f, 0);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(1, 1, 1);
        Vector2 size = new Vector2(600f, 100f);
        
        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        int fontSize = 24;
        TextAnchor alignment = TextAnchor.UpperCenter;//MiddleCenter;

        //GameObject sub = GameObject.Find("Subtitle");
        //if (sub != null)
        //    Object.Destroy(sub);
        //GameObject.Find("BottomPanel").gameObject.SetActive(true);
        GameObject display = GameObject.Find(canvas);
        //GameObject bp = GameObject.Instantiate(Resources.Load("BottomPanel"), display.transform) as GameObject;
        display.GetComponent<Canvas>().enabled = true;

        GameObject sub = new GameObject("Subtitle");
        
        sub.transform.SetParent(display.transform);

        Text content = sub.AddComponent<Text>();
        content.rectTransform.anchoredPosition3D = position;
        content.rectTransform.anchorMax = anchorMax;
        content.rectTransform.anchorMin = anchorMin;
        content.rectTransform.pivot = pivot;
        content.rectTransform.localRotation = rotation;
        content.rectTransform.sizeDelta = size;
        content.rectTransform.localScale = scale;
        content.font = font;
        content.fontSize = fontSize;
        content.fontStyle = FontStyle.Bold;
        content.alignment = alignment;
        content.horizontalOverflow = HorizontalWrapMode.Wrap;
        content.verticalOverflow = VerticalWrapMode.Overflow;

        content.text = s;
        return sub;
    }
    public static GameObject infoText(string s, string canvas)
    {
        Vector3 position = new Vector3(0, -180f, 0); //x, y, z
        Vector2 anchorMax = new Vector2(0.5f, 1);
        Vector2 anchorMin = new Vector2(0.5f, 1);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(1, 1, 1);
        Vector2 size = new Vector2(600f, 100f);

        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        int fontSize = 24;
        TextAnchor alignment = TextAnchor.UpperCenter;//MiddleCenter;

        //GameObject sub = GameObject.Find("Subtitle");
        //if (sub != null)
        //    Object.Destroy(sub);
        //GameObject.Find("BottomPanel").gameObject.SetActive(true);
        GameObject display = GameObject.Find(canvas);
        //GameObject bp = GameObject.Instantiate(Resources.Load("BottomPanel"), display.transform) as GameObject;
        display.GetComponent<Canvas>().enabled = true;

        GameObject sub = new GameObject("Subtitle");

        sub.transform.SetParent(display.transform);

        Text content = sub.AddComponent<Text>();
        content.rectTransform.anchoredPosition3D = position;
        content.rectTransform.anchorMax = anchorMax;
        content.rectTransform.anchorMin = anchorMin;
        content.rectTransform.pivot = pivot;
        content.rectTransform.localRotation = rotation;
        content.rectTransform.sizeDelta = size;
        content.rectTransform.localScale = scale;
        content.font = font;
        content.fontSize = fontSize;
        content.fontStyle = FontStyle.Bold;
        content.alignment = alignment;
        content.horizontalOverflow = HorizontalWrapMode.Wrap;
        content.verticalOverflow = VerticalWrapMode.Overflow;

        content.text = s;
        return sub;
    }

    public static GameObject notice(string s)
    {
        Vector3 position = new Vector3(60f, -100f, 0); //x, y, z
        Vector2 anchorMax = new Vector2(0.5f, 1f);
        Vector2 anchorMin = new Vector2(0.5f, 1f);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(1f, 1f, 1f);
        Vector2 size = new Vector2(300f, 100f);

        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        int fontSize = 28;
        TextAnchor alignment = TextAnchor.UpperCenter;

        //GameObject sub = GameObject.Find("Subtitle");
        //if (sub != null)
        //    Object.Destroy(sub);
        GameObject notis = new GameObject("Notice");

        notis.transform.SetParent(GameObject.Find("UI Display").transform);

        Text content = notis.AddComponent<Text>();
        content.rectTransform.anchoredPosition3D = position;
        content.rectTransform.anchorMax = anchorMax;
        content.rectTransform.anchorMin = anchorMin;
        content.rectTransform.pivot = pivot;
        content.rectTransform.localRotation = rotation;
        content.rectTransform.sizeDelta = size;
        content.rectTransform.localScale = scale;
        content.font = font;
        content.fontSize = fontSize;
        content.alignment = alignment;
        content.horizontalOverflow = HorizontalWrapMode.Overflow;

        content.text = s;
        return notis;
    }

    public static void addResetText()
    {
        Vector3 position = new Vector3(100f, -50f, 0); //x, y, z
        Vector2 anchorMax = new Vector2(0, 1f);
        Vector2 anchorMin = new Vector2(0, 1f);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(1f, 1f, 1f);
        Vector2 size = new Vector2(120f, 100f);

        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        int fontSize = 16;
        TextAnchor alignment = TextAnchor.UpperCenter;

        //GameObject sub = GameObject.Find("Subtitle");
        //if (sub != null)
        //    Object.Destroy(sub);
        GameObject notis = new GameObject("ResetText");

        notis.transform.SetParent(GameObject.Find("UI Display").transform);

        Text content = notis.AddComponent<Text>();
        content.rectTransform.anchoredPosition3D = position;
        content.rectTransform.anchorMax = anchorMax;
        content.rectTransform.anchorMin = anchorMin;
        content.rectTransform.pivot = pivot;
        content.rectTransform.localRotation = rotation;
        content.rectTransform.sizeDelta = size;
        content.rectTransform.localScale = scale;
        content.font = font;
        content.fontSize = fontSize;
        content.alignment = alignment;
        content.horizontalOverflow = HorizontalWrapMode.Overflow;

        content.text = "Press R to Reset";
    }

    public static void DestroyTexts(GameObject g, string canvas)
    {
        GameObject.Destroy(g);
        GameObject.Find(canvas).GetComponent<Canvas>().enabled = false;
    }

    public static void printMultipleLines(string[] s, string canvas)
    {
        GameObject display = GameObject.Find(canvas);
        //GameObject sub = null;
        //int i = s.Length;
        display.GetComponent<Canvas>().enabled = true;
        for (int i = 0; i < s.Length; i++)
        {
            printAfterTime(s[i], canvas, i + 3f);
        }
        /* while (i > 0) {
            if (sub == null)
            {
                sub = subtitle(s[s.Length - i], canvas);
                GameObject.Destroy(sub, 3f);
                i -= 2;
            }
        }*/
        
        /*for (int i = 0; i < s.Length; i++)
        {
            sub = subtitle(s[i], canvas);
            printOneLine(sub);
            //for (int j = 0; j < 1000000; j++) ;
            //GameObject.Destroy(sub);
        }*/
        //display.GetComponent<Canvas>().enabled = false;
    }
    public static void addBlackScreen()
    {
        if (GameObject.Find("blackCurtain"))
        {
            Object.Destroy(GameObject.Find("blackCurtain"));
        }

        GameObject curtain = new GameObject("blackCurtain");
        curtain.transform.SetParent(GameObject.Find("UI Display").transform);
        Image c = curtain.AddComponent<Image>();
        c.color = new Color(0,0,0,255);
        c.rectTransform.localScale = new Vector3(50, 50, 50);
        Object.Destroy(c, 0.5f);
    }
	
}
