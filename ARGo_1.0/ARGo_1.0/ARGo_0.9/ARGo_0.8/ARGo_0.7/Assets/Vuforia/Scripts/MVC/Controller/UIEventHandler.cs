using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Vuforia;
using GooglePlayGames;
using Facebook.Unity;
using UnityEngine.SocialPlatforms;
using System.Net;

public class UIEventHandler : MonoBehaviour
{

    ServerController server;
    string shop_id = "";
    public GameObject ClosedMenu;
    public Texture[] left_right = new Texture[2];
    public Dropdown dropdownitem;
    public GameObject itemcontainer; //반복시킬 Panel
    public GameObject itemPrefab; //반복시킬 Panel
    public int columnCount = 1;
    public Text Name_Text;
    public Text Price_Text;
    private List<GameObject> infopanel = new List<GameObject>();
    public Animator anim;
    private bool animate = false;
    private int FilteringIndex = 0;
    public Text ShowState;
    public Button URLbutton;
    public Texture[] star = new Texture[3];
    public Texture[] animbutton = new Texture[2];
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Image showstatus;
    LoginAndNewUser LData;
    public Button back_button;

    // Use this for initialization
    void Start()
    {
        Debug.Log("UI컨트롤러");
        Screen.SetResolution(800, 600, true);
        if (showstatus != null)
        {
            ShowState.text = "";
            showstatus.enabled = false;
        }
    }

    public void Show_BackButton(bool back)
    {
        if (back == true)
        {
            URLbutton.transform.Translate(0.0f, 700.0f, 0.0f);
            back_button.transform.Translate(0.0f, -650.0f, 0.0f);
        }

        else
        {
            URLbutton.transform.Translate(0.0f, -700.0f, 0.0f);
            back_button.transform.Translate(0.0f, 650.0f, 0.0f);
        }
    }
    /* 종료 키 입력 처리 */
    //테스트를 위해 여러 키입력을 추가
    public void Connect(ServerController obj)
    {

        server = FindObjectOfType<ServerController>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }

    //뷰포리아 초기화면에서 클릭시
    public void startVuforia()  //뷰포리아 초기화를 위해 필요
    {                           //초기화를 안하면 카메라 로드할 때 오류발생
        SceneManager.LoadScene(1);
    }
    public void login()//로그인하기
    {
        if (server == null)
        {
            gameObject.AddComponent<ServerController>();
            server = FindObjectOfType<ServerController>();
            server.initial();
        }
        server.Login();

        /*
        LData = FindObjectOfType<LoginAndNewUser>();

        string check = new WebClient().DownloadString("http://52.78.20.5/login_db_test.php?id=" + LData.getid() + "&pw=" + LData.getpw());
        Debug.Log(check);

        if (check != "")
        {
            SceneManager.LoadScene(3);
        }
        */
    }
    
  

    public void move_new_user()//회원가입하기
    {
        SceneManager.LoadScene(2);
    }

    public void move_login()
    {
        SceneManager.LoadScene(1);
    }
    public void move_initial()
    {
        SceneManager.LoadScene(0);
    }
    public void new_user()
    {
        if (server == null)
        {
            gameObject.AddComponent<ServerController>();
            server = FindObjectOfType<ServerController>();
            server.initial();
        }
        server.new_user();
    }
    public void setMenu(Color c)
    {
        menu.color = c;
        URLbutton.interactable = true;
        dropdownitem.interactable = true;
        dropdownitem.GetComponentInChildren<Text>().text = "전체";
    }
    public void Panel_Opened()
    {
        if (animate == false)
        {
            anim.SetBool("iDisplayed", true);
            animate = true;
            ClosedMenu.GetComponent<RawImage>().texture = left_right[1];
        }
        else
        {
            anim.SetBool("iDisplayed", false);
            animate = false;
            ClosedMenu.GetComponent<RawImage>().texture = left_right[0];
        }
    }
    public void setButtinText(Button b)
    {
        if (animate)
        {
            animate = false;
            b.GetComponentInChildren<RawImage>().texture = animbutton[0];
        }
        else
        {
            animate = true;
            b.GetComponentInChildren<RawImage>().texture = animbutton[1];
        }
    }
    public void settingdropdown(string[] categoryList)
    {
        if(categoryList.Length==0){
            dropdownitem.interactable = false;
            URLbutton.interactable = false;
            setState("빈 메뉴판 입니다\n다른 메뉴판을 인식시키세요");
            return;
        }
        dropdownitem.options.Clear();
        dropdownitem.options.Add(new Dropdown.OptionData("전체"));
        for (int i = 0; i < categoryList.Length; i++)
        {
            dropdownitem.options.Add(new Dropdown.OptionData(categoryList[i]));
        }
    }
    public void setScrollbar(Scrollbar[] b)
    {
        for (int i=0;i<b.Length;i++)
        {
            b[i].value = 0.999f;
        }
    }
    public void setState(string text2)
    {
        ShowState.text = text2;

    }
    public void setState2(string text2)
    {
        ShowState.text = text2;
        showstatus.GetComponent<UnityEngine.UI.Image>().enabled = true;
        if (text2.Equals(""))
        {
            showstatus.GetComponent<UnityEngine.UI.Image>().enabled = false;

        }
    }
    public void filter()
    {
        FilteringIndex = dropdownitem.value;
        server.filteringdata(FilteringIndex);
    }
    public void settingList(int itemCount, string[] name, int[] price, int[] rating)
    {
        for (int i = 0; i < infopanel.Count; i++)
        {
            Destroy(infopanel[i], 0);
        }
        infopanel.Clear();
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = itemcontainer.GetComponent<RectTransform>();

        Name_Text.text = name[0];
        Price_Text.text = price[0].ToString();

        float width = containerRectTransform.rect.width / columnCount;//700
        float ratio = width / rowRectTransform.rect.width;//1
        float height = rowRectTransform.rect.height * ratio;//70
        int rowCount = itemCount / columnCount;

        if (itemCount % rowCount > 0)
            rowCount++;

        float scrollHeight = height * rowCount;//50 * 갯수
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -10);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight+10);

        int j = 0;

        for (int i = 0; i < itemCount; i++)
        {
            if (i % columnCount == 0)
                j++;

            GameObject newItem = Instantiate(itemPrefab) as GameObject;

            newItem.name = gameObject.name + "item at (" + i + "," + j + ")";
            if (i != itemCount - 1)
            {
                Name_Text.text = name[i + 1];
                Price_Text.text = price[i + 1].ToString();
            }
            newItem.transform.SetParent(itemcontainer.transform);// = itemcontainer.transform;
            newItem.AddComponent<ClickAction>();
            newItem.GetComponent<ClickAction>().setcount(i);
            infopanel.Add(newItem);
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            RawImage[] image = new RawImage[5];
            image = rectTransform.GetComponentsInChildren<RawImage>();
            for (int w = 0; w < 5; w++)
            {
                if (rating[i] / 2 >= 2)
                {
                    rating[i] -= 2;
                    image[w].texture = star[1];
                }
                else if (rating[i] % 2 == 1)
                {
                    rating[i] = 0;
                    image[w].texture = star[2];
                }
                else
                    image[w].texture = star[0];
            }
            float x = -containerRectTransform.rect.width / 2;// + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
        setScrollbar(FindObjectsOfType<Scrollbar>());
    }

    public void exit()
    {
        Application.Quit();
    }
    public void ClickedPanel(int index)
    {
        //클릭된 음식을 띄움
        //필터링 됬을 수도 있기 때문에 맞는 이미지를 찾아줘야함
        Panel_Opened();
        server.DownloadImageFromFilteringIndex(index, FilteringIndex);
    }
    public void setShopID(string id)
    {
        shop_id = id;
    }
    public void GetNaverInfo()
    {
        string url = "https://store.naver.com/restaurants/detail?id=" + shop_id;

        Application.OpenURL(url);

    }
}
class ClickAction : MonoBehaviour, IPointerClickHandler
{
    private UIEventHandler uc;
    private int number = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        uc.ClickedPanel(number);
    }
    public void setcount(int count)
    {
        number = count;
        uc = FindObjectOfType<UIEventHandler>();
    }
}