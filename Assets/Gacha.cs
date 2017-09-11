using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;

public class Card
{
    public int id;
    public string name;
    public int rare_type;
    public string img_path;
}

public class Gacha : MonoBehaviour
{
    public Text cardName;
    public Image cardImg;

    public void execute()
    {
        StartCoroutine(gacha());
    }

    IEnumerator gacha()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/gacha/execute.json");
        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                string text = request.downloadHandler.text;
                Card card = JsonMapper.ToObject<Card>(text);

                UnityWebRequest imgRequest = new UnityWebRequest(card.img_path);
                DownloadHandlerTexture texDL = new DownloadHandlerTexture(true);
                imgRequest.downloadHandler = texDL;

                yield return imgRequest.Send();
                if (!imgRequest.isNetworkError)
                {
                    Texture2D t = texDL.texture;
                    cardName.text = card.name;
                    cardImg.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero);
                }
            }
        }
    }

}
