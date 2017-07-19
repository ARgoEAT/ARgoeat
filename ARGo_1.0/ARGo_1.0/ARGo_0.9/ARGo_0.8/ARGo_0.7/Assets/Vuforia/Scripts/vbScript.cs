using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class vbScript : MonoBehaviour, IVirtualButtonEventHandler
{
    ServerController server;
    private FoodData foodData;

    private GameObject btn_right;
    private GameObject btn_left;
    private GameObject btn_confirm;
    private GameObject review_panel;

    private TextMesh text_food;
    private TextMesh text_category;
    private TextMesh text_final;

    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    private GameObject star4;
    private GameObject star5;
    public Texture[] mats = new Texture[3];

    private GameObject final_plane;

    private GameObject blank_plane;
    private GameObject mask_plane;

    public static int version = 1;
    public int count = 0;
    private int category_index = 0;
    private bool flag = false;
    public AssetBundle bundle;

    private string[] category;
    private string[] food_name;
    private int[] food_id;

    private int confirm_index = -1;
    private int food_index = 0;
    private int star_index = 0;
    
    /*
    void Awake()
    {
        Application.targetFrameRate = 50;
    }
    */

    void Start()
    {
        review_panel = GameObject.Find("review_panel");

        btn_left = GameObject.Find("btn_left");
        btn_left.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        btn_right = GameObject.Find("btn_right");
        btn_right.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        btn_confirm = GameObject.Find("btn_confirm");
        btn_confirm.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        text_category = GameObject.Find("text_category").GetComponent<TextMesh>();
        text_food = GameObject.Find("text_food").GetComponent<TextMesh>();
        text_final = GameObject.Find("Final_Confirm").GetComponent<TextMesh>();
        

        star1 = GameObject.Find("star1");
        star2 = GameObject.Find("star2");
        star3 = GameObject.Find("star3");
        star4 = GameObject.Find("star4");
        star5 = GameObject.Find("star5");

        final_plane = GameObject.Find("Final_Plane");

        
        blank_plane = GameObject.Find("Blank_Plane");
        mask_plane = GameObject.Find("Mask");
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.A))
        {
            final_plane.transform.Translate(-3.6f, 0.0f, 0.0f);
        }
        */
    }

    public void Back_State()
    {
        SoundManager.instance.PlaySound(4);

        if (confirm_index < 0)
        {

        }

        else if (confirm_index == 0)
        {
            confirm_index = -1;

            category_index = 0;
            text_category.text = category[category_index];
        }

        else if (confirm_index == 1)
        {
            mask_plane.transform.Translate(0.0f, 0.0f, -0.20f);
            blank_plane.transform.Translate(0.0f, 0.0f, -0.20f);

            confirm_index = -1;

            category_index = 0;
            text_category.text = category[category_index];
            text_food.text = "- 메뉴 이름 -";
        }

        else if (confirm_index == 2)
        {
            mask_plane.transform.Translate(0.0f, 0.0f, -0.36f);
            blank_plane.transform.Translate(0.0f, 0.0f, -0.36f);

            confirm_index = -1;

            category_index = 0;
            text_category.text = category[category_index];
            text_food.text = "- 메뉴 이름 -";

            star_index = 6;

            star1.GetComponent<Renderer>().material.mainTexture = mats[2];
            star2.GetComponent<Renderer>().material.mainTexture = mats[2];
            star3.GetComponent<Renderer>().material.mainTexture = mats[2];
            star4.GetComponent<Renderer>().material.mainTexture = mats[0];
            star5.GetComponent<Renderer>().material.mainTexture = mats[0];
        }

        else if(confirm_index == 3)
        {
            mask_plane.transform.Translate(0.0f, 0.0f, -0.52f);
            blank_plane.transform.Translate(0.0f, 0.0f, -0.52f);

            confirm_index = -1;

            category_index = 0;
            text_category.text = category[category_index];
            text_food.text = "- 메뉴 이름 -";

            star_index = 6;

            star1.GetComponent<Renderer>().material.mainTexture = mats[2];
            star2.GetComponent<Renderer>().material.mainTexture = mats[2];
            star3.GetComponent<Renderer>().material.mainTexture = mats[2];
            star4.GetComponent<Renderer>().material.mainTexture = mats[0];
            star5.GetComponent<Renderer>().material.mainTexture = mats[0];

            text_final.text = "";
        }



    }

    public void Connect(ServerController obj)
    {
        server = FindObjectOfType<ServerController>();
        //server = obj;
        //if (server != null) { Debug.Log("UI컨트롤러 : 서버컨트롤러 발견 성공"); }
    }

    public void getCategory(string[] categoryList)
    {
        category = new string[categoryList.Length];
        category[0] = "- 카테고리 -";

        for (int i = 1; i < categoryList.Length; i++)
        {
            category[i] = categoryList[i-1];
        }

        Debug.Log("category[0] : " + category[0]);

        text_category.text = category[0];
    }

    public void setFoodList(int itemCount, string[] foodname, int[] id)
    {
        food_name = new string[itemCount];

        for (int i = 0; i < itemCount; i++)
        {
            food_name[i] = foodname[i];
        }
        text_food.text = food_name[0];

        food_id = new int[itemCount];

        for (int i = 0; i < itemCount; i++)
        {
            food_id[i] = id[i];
        }
    }
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        /*
        if (flag == false)
        {
            flag = true;
            foodData = FindObjectOfType<FoodData>();
            Debug.Log("푸드넣음");
        }
        */
        if (confirm_index == -1)
        {
            server.vbgetcate();
            confirm_index++;
        }

        else if (confirm_index == 0 && vb.VirtualButtonName == "btn_left")
        {
            Debug.Log("Confirm == 0 / LEFT");

            SoundManager.instance.PlaySound(0);

            category_index--;

            if (category_index >= 1)
            {
                text_category.text = category[category_index];
            }

            else
            {
                category_index = category.Length - 1;
                text_category.text = category[category_index];
            }
        }

        else if (confirm_index == 0 && vb.VirtualButtonName == "btn_right")
        {
            Debug.Log("Confirm == 0 / RIGHT");

            SoundManager.instance.PlaySound(0);

            category_index++;

            if (category_index <= (category.Length - 1))
            {
                text_category.text = category[category_index];
            }

            else
            {
                category_index = 1;
                text_category.text = category[category_index];
            }
        }

        else if (confirm_index == 0 && vb.VirtualButtonName == "btn_confirm")
        {
            if (category_index != 0)
            {
                SoundManager.instance.PlaySound(1);

                Debug.Log("Confirm == 0 / Confirm");
                confirm_index++;
                server.filterlingdata_Review(category_index);

                mask_plane.transform.Translate(0.0f, 0.0f, 0.20f);
                blank_plane.transform.Translate(0.0f, 0.0f, 0.20f);
            }

            else
            {
                SoundManager.instance.PlaySound(3);
            }
        }

        else if (confirm_index == 1 && vb.VirtualButtonName == "btn_left")
        {
            Debug.Log("Confirm == 1 / LEFT");

            SoundManager.instance.PlaySound(0);

            food_index--;

            if (food_index >= 0)
            {
                text_food.text = food_name[food_index];
            }

            else
            {
                food_index = food_name.Length - 1;
                text_food.text = food_name[food_index];
            }
        }

        else if (confirm_index == 1 && vb.VirtualButtonName == "btn_right")
        {
            Debug.Log("Confirm == 1 / RIGHT");

            SoundManager.instance.PlaySound(0);

            food_index++;

            if (food_index <= (food_name.Length - 1))
            {
                text_food.text = food_name[food_index];
            }

            else
            {
                food_index = 0;
                text_food.text = food_name[food_index];
            }
        }

        else if (confirm_index == 1 && vb.VirtualButtonName == "btn_confirm")
        {
            SoundManager.instance.PlaySound(1);

            Debug.Log("Confirm == 1 / Confirm");
            confirm_index++;

            mask_plane.transform.Translate(0.0f, 0.0f, 0.16f);
            blank_plane.transform.Translate(0.0f, 0.0f, 0.16f);

            star_index = 6;

            star1.GetComponent<Renderer>().material.mainTexture = mats[2];
            star2.GetComponent<Renderer>().material.mainTexture = mats[2];
            star3.GetComponent<Renderer>().material.mainTexture = mats[2];
            star4.GetComponent<Renderer>().material.mainTexture = mats[0];
            star5.GetComponent<Renderer>().material.mainTexture = mats[0];
        }

        else if (confirm_index == 2 && vb.VirtualButtonName == "btn_left")
        {
            star_index -= 2;

            if (star_index == 0)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[0];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 1)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[1];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 2)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 3)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[1];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 4)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 5)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[1];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 6)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 7)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[1];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 8)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 9)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[1];
            }

            else if (star_index == 10)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[2];
            }

            else
            {
                SoundManager.instance.PlaySound(3);
                star_index = 0;
            }
        }

        else if (confirm_index == 2 && vb.VirtualButtonName == "btn_right")
        {
            star_index += 2;

            if (star_index == 0)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[0];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 1)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[1];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 2)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[0];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 3)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[1];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 4)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[0];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 5)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[1];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 6)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[0];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 7)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[1];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 8)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[0];
            }

            else if (star_index == 9)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[1];
            }

            else if (star_index == 10)
            {
                SoundManager.instance.PlaySound(0);

                star1.GetComponent<Renderer>().material.mainTexture = mats[2];
                star2.GetComponent<Renderer>().material.mainTexture = mats[2];
                star3.GetComponent<Renderer>().material.mainTexture = mats[2];
                star4.GetComponent<Renderer>().material.mainTexture = mats[2];
                star5.GetComponent<Renderer>().material.mainTexture = mats[2];
            }

            else
            {
                SoundManager.instance.PlaySound(3);
                star_index = 10;
            }
        }

        else if (confirm_index == 2 && vb.VirtualButtonName == "btn_confirm")
        {
            SoundManager.instance.PlaySound(1);

            confirm_index++;
            mask_plane.transform.Translate(0.0f, 0.0f, 0.16f);
            blank_plane.transform.Translate(0.0f, 0.0f, 0.16f);
            text_final.text = "- 평가완료 -";
        }

        else if (confirm_index == 3)
        {
            SoundManager.instance.PlaySound(2);

            confirm_index++;

            Debug.Log("보낼 아이디 : " + food_id[food_index]);
            server.StartUpdate(food_id[food_index], star_index);

            review_panel.SetActive(false); // 비활성화
            final_plane.transform.Translate(-3.6f, 0.0f, 0.0f);
        }

        /*
        else
        {
            if (vb.VirtualButtonName == "btn_left")
            {
                if (i != 0) i--;
                text_food.text = foodData.GetRealName(i);
                SoundManager.instance.PlaySound();
                Debug.Log("LEFT");
            }

            else if (vb.VirtualButtonName == "btn_right")
            {
                i++;
                text_food.text = foodData.GetRealName(i);
                SoundManager.instance.PlaySound();
                Debug.Log("RIGHT");
            }

            else if (vb.VirtualButtonName == "btn_confirm")
            {
                //menu_index++;
                //Debug.Log(menu_index);
                SoundManager.instance.PlaySound();
                Debug.Log("CONFIRM");
            }
        }
        */
    }


    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        //panel.SetActive(false); // 비활성화
        //Debug.Log("Buttion Released!!!");
    }


}