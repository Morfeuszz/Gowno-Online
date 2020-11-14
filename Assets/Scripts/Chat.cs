using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{

    public class ChatMessage {
        public string action = "chatMessage";
        public string Username;
        public string message;
    }



    [System.Serializable]
    public class Message{
        public string text;
        public Text textObject;
    }
    public GameObject chatPanel, textObject;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    public static ChatMessage message = new ChatMessage();
    public Connection connection;
    public PlayerControler playerController;
    public InputField chatInput;
    bool inputMode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatInputMode(true);
        }
        if(chatInput.isFocused == true && inputMode == false){
            chatInputMode(false);
        } else if (chatInput.isFocused == false && inputMode == true){
            chatInputMode(false);
        }
    }
    public void sendChatMessage(){
        if(chatInput.text.Replace(" ","") != ""){
        message.Username = connection.DataController.data.Username;
        message.message = chatInput.text;
        displayChatMessage();
        connection.SendWebSocketMessage(JsonUtility.ToJson(message));
        }
    }

    public void reciveChatMessage(string recvmessage){
        message = JsonUtility.FromJson<ChatMessage>(recvmessage);
        displayChatMessage();
    }

    void displayChatMessage(){
        Message newMessage = new Message();
        newMessage.text = message.Username + ": " + message.message;
        //newMessage.text = "Morfina" + ": " + chatInput.text;
        chatInput.text = "";
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    void chatInputMode(bool enter){
        if(!inputMode){
            chatInput.Select();
            playerController.canMove = false;
            inputMode = true;
        } else{
            if(chatInput.text.Replace(" ","") != "" && enter == true){
                sendChatMessage();
            }
            chatInput.interactable = false;
            chatInput.interactable = true;
            playerController.canMove = true;
            inputMode = false;
        }

    }
}
