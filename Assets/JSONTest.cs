using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.Networking;

public class User
{
    public int id;
    public string name;
    public string created_at;
    public string updated_at;
    public string url;
}

public class UserCard
{
    public int userId;
    public int cardId;
}

public class JSONTest : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(GetUser());
    }

    IEnumerator GetUser()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/users/1.json");
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
                Debug.Log(text);
                User user = JsonMapper.ToObject<User>(text);
                Debug.Log(user.name);
            }
        }
    }
}
