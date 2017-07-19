using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct food
{
    /* 테스트를 위해 퍼블릭으로 설정. 후에 겟셋으로 바꾸면 좋을듯 */
    public int food_id;
    public string food_name;
    public int food_price;
    public string food_url;
    public float food_rating;
    public int count_rating;
    public string Category;
    public string real_name;

    public food(int id, string fname, int fprice, string url, float rate, int cntrate, string Cate, string rname)
    {
        food_id = id;
        food_name = fname;
        food_price = fprice;
        food_url = url;
        food_rating = rate;
        count_rating = cntrate;
        Category = Cate;
        real_name = rname;
    }
}

public class FoodData : MonoBehaviour
{
    private List<food> datamanager = new List<food>();
    private List<string> cate = new List<string>();
    //정렬된 데이터를 출력하기 위한 변수들 필요
    public int count;
    public int filteringcount;
    private string MenuName = "";
    public string bundleURL;
    //모든 URL과 bundle을 저장할 변수
    //정렬된 URL과 bundle을 저장할 변수

    void Start()
    {

    }

    public void SetFoodData(food f)
    {
        datamanager.Add(f);
        count = datamanager.Count;
    }


    public void SetMenuName(string name)
    {
        MenuName = name;
    }


    public int getID(int index)
    {
        return datamanager[index].food_id;
    }


    public string GetURL(int bundlecount)
    {
        return datamanager[bundlecount].food_url;
    }


    public string GetBundleName(int bundlecount)
    {
        return datamanager[bundlecount].food_name;
    }



    public string GetRealName(int bundlecount)
    {
        return datamanager[bundlecount].real_name;
    }


    public string[] GetAllFoodName()
    {
        string[] allname = new string[count];
        for (int i = 0; i < count; i++)
            allname[i] = datamanager[i].food_name;
        return allname;
    }
    public string[] GetAllName()
    {
        string[] allname = new string[count];
        for (int i = 0; i < count; i++)
            allname[i] = datamanager[i].real_name;
        return allname;
    }
    public string[] GetAllCategory()
    {
        if (count == 0)
        {
            return null;
        }
        string[] catelist = new string[cate.Count];
        for (int i = 0; i < cate.Count; i++)
        {
            catelist[i] = cate[i];
        }
        return catelist;
    }
    public string GetCategory(int index)
    {
        return cate[index];
    }
    public void SortByRating()
    {
        food temp;
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                if (datamanager[i].food_rating < datamanager[j].food_rating)
                {
                    temp = datamanager[i];
                    datamanager[i] = datamanager[j];
                    datamanager[j] = temp;
                }
            }
        }
    }
    public void setCategory()
    {
        if (count == 0)
        {
            return;
        }
        cate.Add(datamanager[0].Category);
        bool dif = false;
        for (int i = 1; i < count; i++)
        {
            dif = false;
            for (int j = 0; j < cate.Count; j++)
            {
                if (cate[j].Equals(datamanager[i].Category))
                {
                    break;
                }
                if (j == cate.Count - 1)
                {
                    dif = true;
                }
            }
            if (dif)
            {
                cate.Add(datamanager[i].Category);
            }
        }
    }
    public int GetIndexByFilteringIndex(int index, string category)
    {
        for (int i = 0; i < count; i++)
        {
            if (datamanager[i].Category.Equals(category))
            {
                if (index == 0)
                {
                    return i;
                }
                else
                    index--;
            }
        }
        return 0;
    }
    public int[] GetAllRate()
    {
        int[] rate = new int[count];
        for (int i = 0; i < count; i++)
        {
            rate[i] = (int)datamanager[i].food_rating;
        }
        return rate;
    }
    public string[] GetAllNameByCategory(int index)
    {
        string filter = cate[index];
        List<string> filt = new List<string>();
        for (int i = 0; i < count; i++)
        {
            if (datamanager[i].Category.Equals(filter))
            {
                filt.Add(datamanager[i].real_name);
            }
        }
        string[] output = new string[filt.Count];
        for (int i = 0; i < filt.Count; i++)
        {
            output[i] = filt[i];
        }
        filteringcount = filt.Count;
        return output;
    }
    public int[] GetAllIDByCategory(int index)
    {
        string filter = cate[index];
        List<int> filt = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (datamanager[i].Category.Equals(filter))
            {
                filt.Add(datamanager[i].food_id);
            }
        }
        int[] output = new int[filt.Count];
        for (int i = 0; i < filt.Count; i++)
        {
            output[i] = filt[i];
        }
        return output;
    }
    public int[] GetAllPriceByCategory(int index)
    {
        string filter = cate[index];
        List<int> filt = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (datamanager[i].Category.Equals(filter))
            {
                filt.Add(datamanager[i].food_price);
            }
        }
        int[] output = new int[filt.Count];
        for (int i = 0; i < filt.Count; i++)
        {
            output[i] = filt[i];
        }
        return output;
    }
    public int[] GetAllRatingByCategory(int index)
    {
        string filter = cate[index];
        List<int> filt = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (datamanager[i].Category.Equals(filter))
            {
                filt.Add((int)datamanager[i].food_rating);
            }
        }
        int[] output = new int[filt.Count];
        for (int i = 0; i < filt.Count; i++)
        {
            output[i] = filt[i];
        }
        return output;
    }
    public int[] GetAllPrice()
    {
        int[] allprice = new int[count];
        for (int i = 0; i < count; i++)
            allprice[i] = datamanager[i].food_price;
        return allprice;
    }
    public string GetMenuName()
    {
        return MenuName;
    }
    public void setNull()
    {
        MenuName = "";
        filteringcount = 0;
        datamanager = null;
        datamanager = new List<food>();
        cate = null;
        cate = new List<string>();
    }
}