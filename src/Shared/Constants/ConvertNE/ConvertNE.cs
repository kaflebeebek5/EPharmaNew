using System;

public class ConvertNE
{
    public ConvertNE()
    {
    }
    #region fields
    private bool _checkRange = true;
    public bool CheckRange { get { return _checkRange; } set { _checkRange = value; } }
    //int[,] r2 = new int[,] {{1, 2, 3}, {4, 5, 6}};
    private int[,] bs = new int[,]{
            {1977,30,32,31,32,31,30,30,30,29,30,29,31},
            {1978,31,31,32,31,31,31,30,29,30,29,30,30},
            {1979,31,31,32,32,31,30,30,29,30,29,30,30},
            {1980,31,32,31,32,31,30,30,30,29,29,30,31},
            {1981,31,31,31,32,31,31,29,30,30,29,29,31},
            {1982,31,31,32,31,31,31,30,29,30,29,30,30},
            {1983,31,31,32,32,31,30,30,29,30,29,30,30},
            {1984,31,32,31,32,31,30,30,30,29,29,30,31},
            {1985,31,31,31,32,31,31,29,30,30,29,30,30},
            {1986,31,31,32,31,31,31,30,29,30,29,30,30},
            {1987,31,31,32,32,31,30,30,29,30,29,30,30},
            {1988,31,32,31,32,31,30,30,30,29,29,30,31},
            {1989,31,31,31,32,31,31,29,30,30,29,30,30},
            {1990,31,31,32,31,31,31,30,29,30,29,30,30},
            {1991,31,32,31,32,31,30,30,30,29,29,30,30},
            {1992,31,32,31,32,31,30,30,30,29,30,29,31},
            {1993,31,31,31,32,31,31,30,29,30,29,30,30},
            {1994,31,31,32,31,31,31,30,29,30,29,30,30},
            {1995,31,32,31,32,31,30,30,30,29,29,30,30},
            {1996,31,32,31,32,31,30,30,30,29,30,29,31},
            {1997,31,31,32,31,31,31,30,29,30,29,30,30},
            {1998,31,31,32,31,31,31,30,29,30,29,30,30},
            {1999,31,32,31,32,31,30,30,30,29,29,30,31},
            {2000,30,32,31,32,31,30,30,30,29,30,29,31},
            {2001,31,31,32,31,31,31,30,29,30,29,30,30},
            {2002,31,31,32,32,31,30,30,29,30,29,30,30},
            {2003,31,32,31,32,31,30,30,30,29,29,30,31},
            {2004,30,32,31,32,31,30,30,30,29,30,29,31},
            {2005,31,31,32,31,31,31,30,29,30,29,30,30},
            {2006,31,31,32,32,31,30,30,29,30,29,30,30},
            {2007,31,32,31,32,31,30,30,30,29,29,30,31},
            {2008,31,31,31,32,31,31,29,30,30,29,29,31},
            {2009,31,31,32,31,31,31,30,29,30,29,30,30},
            {2010,31,31,32,32,31,30,30,29,30,29,30,30},
            {2011,31,32,31,32,31,30,30,30,29,29,30,31},
            {2012,31,31,31,32,31,31,29,30,30,29,30,30},
            {2013,31,31,32,31,31,31,30,29,30,29,30,30},
            {2014,31,31,32,32,31,30,30,29,30,29,30,30},
            {2015,31,32,31,32,31,30,30,30,29,29,30,31},
            {2016,31,31,31,32,31,31,29,30,30,29,30,30},
            {2017,31,31,32,31,31,31,30,29,30,29,30,30},
            {2018,31,32,31,32,31,30,30,29,30,29,30,30},
            {2019,31,32,31,32,31,30,30,30,29,30,29,31},
            {2020,31,31,31,32,31,31,30,29,30,29,30,30},
            {2021,31,31,32,31,31,31,30,29,30,29,30,30},
            {2022,31,32,31,32,31,30,30,30,29,29,30,30},
            {2023,31,32,31,32,31,30,30,30,29,30,29,31},
            {2024,31,31,31,32,31,31,30,29,30,29,30,30},
            {2025,31,31,32,31,31,31,30,29,30,29,30,30},
            {2026,31,32,31,32,31,30,30,30,29,29,30,31},
            {2027,30,32,31,32,31,30,30,30,29,30,29,31},
            {2028,31,31,32,31,31,31,30,29,30,29,30,30},
            {2029,31,31,32,31,32,30,30,29,30,29,30,30},
            {2030,31,32,31,32,31,30,30,30,29,29,30,31},
            {2031,30,32,31,32,31,30,30,30,29,30,29,31},
            {2032,31,31,32,31,31,31,30,29,30,29,30,30},
            {2033,31,31,32,32,31,30,30,29,30,29,30,30},
            {2034,31,32,31,32,31,30,30,30,29,29,30,31},
            {2035,30,32,31,32,31,31,29,30,30,29,29,31},
            {2036,31,31,32,31,31,31,30,29,30,29,30,30},
            {2037,31,31,32,32,31,30,30,29,30,29,30,30},
            {2038,31,32,31,32,31,30,30,30,29,29,30,31},
            {2039,31,31,31,32,31,31,29,30,30,29,30,30},
            {2040,31,31,32,31,31,31,30,29,30,29,30,30},
            {2041,31,31,32,32,31,30,30,29,30,29,30,30},
            {2042,31,32,31,32,31,30,30,30,29,29,30,31},
            {2043,31,31,31,32,31,31,29,30,30,29,30,30},
            {2044,31,31,32,31,31,31,30,29,30,29,30,30},
            {2045,31,32,31,32,31,30,30,29,30,29,30,30},
            {2046,31,32,31,32,31,30,30,30,29,29,30,31},
            {2047,31,31,31,32,31,31,30,29,30,29,30,30},
            {2048,31,31,32,31,31,31,30,29,30,29,30,30},
            {2049,31,32,31,32,31,30,30,30,29,29,30,30},
            {2050,31,32,31,32,31,30,30,30,29,30,29,31},
            {2051,31,31,31,32,31,31,30,29,30,29,30,30},
            {2052,31,31,32,31,31,31,30,29,30,29,30,30},
            {2053,31,32,31,32,31,30,30,30,29,29,30,30},
            {2054,31,32,31,32,31,30,30,30,29,30,29,31},
            {2055,31,31,32,31,31,31,30,29,30,29,30,30},
            {2056,31,31,32,31,32,30,30,29,30,29,30,30},
            {2057,31,32,31,32,31,30,30,30,29,29,30,31},
            {2058,30,32,31,32,31,30,30,30,29,30,29,31},
            {2059,31,31,32,31,31,31,30,29,30,29,30,30},
            {2060,31,31,32,32,31,30,30,29,30,29,30,30},
            {2061,31,32,31,32,31,30,30,30,29,29,30,31},
            {2062,30,32,31,32,31,31,29,30,29,30,29,31},
            {2063,31,31,32,31,31,31,30,29,30,29,30,30},
            {2064,31,31,32,32,31,30,30,29,30,29,30,30},
            {2065,31,32,31,32,31,30,30,30,29,29,30,31},
            {2066,31,31,31,32,31,31,29,30,30,29,29,31},
            {2067,31,31,32,31,31,31,30,29,30,29,30,30},
            {2068,31,31,32,32,31,30,30,29,30,29,30,30},
            {2069,31,32,31,32,31,30,30,30,29,29,30,31},
            {2070,31,31,31,32,31,31,29,30,30,29,30,30},
            {2071,31,31,32,31,31,31,30,29,30,29,30,30},
            {2072,31,32,31,32,31,30,30,29,30,29,30,30},
            {2073,31,32,31,32,31,30,30,30,29,29,30,31},
            {2074,31,31,31,32,31,31,30,29,30,29,30,30},
            {2075,31,31,32,31,31,31,30,29,30,29,30,30},
            {2076,31,32,31,32,31,30,30,30,29,29,30,30},
            {2077,31,32,31,32,31,30,30,30,29,30,29,31},
            {2078,31,31,31,32,31,31,30,29,30,29,30,30},
            {2079,31,31,32,31,31,31,30,29,30,29,30,30},
            {2080,31,32,31,32,31,30,30,30,29,29,30,30},
            {2081,31,31,32,32,31,30,30,30,29,30,30,30},
            {2082,30,32,31,32,31,30,30,30,29,30,30,30},
            {2083,31,31,32,31,31,30,30,30,29,30,30,30},
            {2084,31,31,32,31,31,30,30,30,29,30,30,30},
            {2085,31,32,31,32,30,31,30,30,29,30,30,30},
            {2086,30,32,31,32,31,30,30,30,29,30,30,30},
            {2087,31,31,32,31,31,31,30,30,29,30,30,30},
            {2088,30,31,32,32,30,31,30,30,29,30,30,30},
            {2089,30,32,31,32,31,30,30,30,29,30,30,30},
            {2090,30,32,31,32,31,30,30,30,29,30,30,30},
            {2091,30,31,31,31,31,31,30,30,29,30,30,30},
            {2092,31,32,31,32,31,30,30,30,29,29,30,31},
            {2093,31,31,31,32,31,31,29,30,30,29,29,31},
            {2094,31,31,32,31,31,31,30,29,30,29,30,30},
            {2095,31,31,32,32,31,30,30,29,30,29,30,30},
            {2092,31,32,31,32,31,30,30,30,29,29,30,31},
            {2093,31,31,31,32,31,31,29,30,30,29,29,31},
            {2094,31,31,32,31,31,31,30,29,30,29,30,30},
            {2095,31,31,32,32,31,30,30,29,30,29,30,30},
            {2096,31,32,31,32,31,30,30,30,29,29,30,31},
            {2097,31,31,31,32,31,31,29,30,30,29,30,30},
            {2098,31,31,32,31,31,31,30,29,30,29,30,30},
            {2099,31,31,32,32,31,30,30,29,30,29,30,30},
            {2100,31,32,31,32,31,30,30,30,29,29,30,31},
            {2101,31,31,31,32,31,31,29,30,30,29,30,30},
            {2102,31,31,32,31,31,31,30,29,30,29,30,30},
            {2103,31,32,31,32,31,30,30,29,30,29,30,30},
            {2104,31,32,31,32,31,30,30,30,29,30,29,31},
            {2105,31,31,31,32,31,31,30,29,30,29,30,30},
            {2106,31,31,32,31,31,31,30,29,30,29,30,30},
            {2107,31,32,31,32,31,30,30,30,29,29,30,30},
            {2108,31,32,31,32,31,30,30,30,29,30,29,31},
            {2109,31,31,32,31,31,31,30,29,30,29,30,30},
            {2110,31,31,32,31,31,31,30,29,30,29,30,30}
    };
    #endregion
    // Calculates wheather english year is leap year or not
    public static bool is_leap_year(int ly)
    {
        int a = ly;
        if (a % 100 == 0)
        {
            if (a % 400 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (a % 4 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool is_range_eng(DateTime d)
    {
        //if (d.Year < 1944 || d.Year > 2033)
        //{
        //    CheckRange = false;
        //}
        if (d.Month < 1 || d.Month > 12)
        {
            CheckRange = false;

        }
        if (d.Day < 1 || d.Day > 31)
        {
            CheckRange = false;
        }
        return CheckRange;
    }
    private bool is_range_nep(int[] a)
    {
        //if (a[2] < 2000 || a[2] > 2091)
        //{
        //    CheckRange = false;
        //}
        if (a[0] < 1 || a[0] > 12)
        {
            CheckRange = false;
        }
        if (a[1] < 1 || a[1] > 32)
        {
            CheckRange = false;
        }
        return CheckRange;
    }

    //currently can only calculate the date between AD 1944-2033...
    public static string ConvertEToN(DateTime eDate)
    {
        string nnDate = "";
        ConvertNE ne = new ConvertNE();
        if (ne.is_range_eng(eDate))
        {
            // english month data.
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //DateTime def_eyy = new DateTime(1944,1,1);
            int def_eyy = 1921;
            //spear head english date...
            DateTime def_nyy = new DateTime(1977, 9, 18 - 1);
            //$def_nyy = 2000; $def_nmm = 9; $def_ndd = 17-1;		//spear head nepali date...

            int total_eDays = 0; int total_nDays = 0; int a = 0; int day = 7 - 1;		//all the initializations...
            int m = 0; int y = 0; int i = 0; int j = 0;
            int numDay = 0;

            // count total no. of days in-terms of year
            for (int x = 0; x < (eDate.Year - def_eyy); x++)
            {	//total days for month calculation...(english)
                if (is_leap_year(def_eyy + x))
                    for (int w = 0; w < 12; w++)
                        total_eDays += lmonth[w];
                else
                    for (int z = 0; z < 12; z++)
                        total_eDays += month[z];
            }
            // count total no. of days in-terms of month					
            for (int p = 0; p < (eDate.Month - 1); p++)
            {
                if (is_leap_year(eDate.Year))
                    total_eDays += lmonth[p];
                else
                    total_eDays += month[p];
            }
            // count total no. of days in-terms of date
            total_eDays += eDate.Day;
            i = 0; j = def_nyy.Month;
            total_nDays = def_nyy.Day;
            m = def_nyy.Month;
            y = def_nyy.Year;

            // count nepali date from array
            while (total_eDays != 0)
            {
                a = ne.bs[i, j];
                total_nDays++;						//count the days
                day++;								//count the days interms of 7 days
                if (total_nDays > a)
                {
                    m++;
                    total_nDays = 1;
                    j++;
                }
                if (day > 7)
                    day = 1;
                if (m > 12)
                {
                    y++;
                    m = 1;
                }
                if (j > 12)
                {
                    j = 1; i++;
                }
                total_eDays--;
            }
            numDay = day;
            //  nnDate = m + "/" + total_nDays + "/" + y;
            nnDate = y + "/" + m + "/" + total_nDays;

        }
        return nnDate;
    }

    // currently can only calculate the date between BS 2000-2089
    public static string ConvertNToE(int[] nn)
    {
        string eeDate = "";
        ConvertNE ne = new ConvertNE();
        DateTime def_eyy = new DateTime(1920, 4, 13 - 1);
        DateTime def_nyy = new DateTime(1977, 1, 1);// equivalent nepali date.
        int total_eDays = 0; int total_nDays = 0; int a = 0; int day = 4 - 1;		// initializations...
        int m = 0; int y = 0;
        //int i = 0;
        int k = 0;

        int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (ne.is_range_nep(nn))
        {
            // count total days in-terms of year
            for (int u = 0; u < (nn[2] - def_nyy.Year); u++)
            {
                for (int v = 1; v <= 12; v++)
                {
                    total_nDays += ne.bs[k, v];
                }
                k++;
            }
            // count total days in-terms of month			
            for (int p = 1; p < nn[0]; p++)
            {
                total_nDays += ne.bs[k, p];
            }

            // count total days in-terms of dat
            total_nDays += nn[1];

            //calculation of equivalent english date...
            total_eDays = def_eyy.Day;
            m = def_eyy.Month;
            y = def_eyy.Year;
            while (total_nDays != 0)
            {
                if (is_leap_year(y))
                {
                    a = lmonth[m - 1];
                }
                else
                {
                    a = month[m - 1];
                }
                total_eDays++;
                day++;
                if (total_eDays > a)
                {
                    m++;
                    total_eDays = 1;
                    if (m > 12)
                    {
                        y++;
                        m = 1;
                    }
                }
                if (day > 7)
                    day = 1;
                total_nDays--;
            }
            eeDate = m + "/" + total_eDays + "/" + y;
        }
        return eeDate;
    }

    public static int GetDaysOfMonth(int y, int m)//y is year 2000 , m is month 1
    {
        int c = -1;
        ConvertNE n = new ConvertNE();
        for (int i = 0; i < 91; i++)
        {
            if (n.bs[i, 0] == y)
            {
                c = i;
                break;
            }
        }
        return n.bs[c, m];//days of month m
    }

    //public static DateTime Get_EOFY(DateTime date)
    //{
    //    string todaydate = ConvertNE.ConvertEToN(date);
    //    string[] str = todaydate.Split('/');
    //    int cyear = int.Parse(str[0]);
    //    int cmonth = int.Parse(str[1]);
    //    int cday = int.Parse(str[2]);
    //    int pyear = 0;

    //    if (cmonth >= 4)
    //    {
    //        pyear = cyear;
    //        cyear = cyear + 1;
    //    }
    //    else
    //    {
    //        pyear = cyear - 1;
    //    }
    //    string date1 = DateStringToInt.GetDate(cyear);
    //    DateTime EOFY = DateTime.Parse(date1).AddDays(-1);
    //    return EOFY;
    //}

    public static DateTime convertNepaliToEnglish(string nepalidate)
    {
        string date = nepalidate;
        int[] a = new int[3];
        int i = 0;
        string[] words = date.Split('/');
        foreach (string word in words)
        {
            if (i == 0)
            {
                a[2] = int.Parse(word);
            }
            else if (i == 1)
            {
                a[0] = int.Parse(word);
            }
            else
            {
                a[1] = int.Parse(word);
            }
            i++;
        }
        DateTime nepday = Convert.ToDateTime(ConvertNToE(a));
        return nepday;
    }

    public static string ConvertEToNWithSlash(DateTime eDate)
    {
        string nnDate = "";
        ConvertNE ne = new ConvertNE();
        if (ne.is_range_eng(eDate))
        {
            // english month data.
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //DateTime def_eyy = new DateTime(1944,1,1);
            int def_eyy = 1921;
            //spear head english date...
            DateTime def_nyy = new DateTime(1977, 9, 18 - 1);
            //$def_nyy = 2000; $def_nmm = 9; $def_ndd = 17-1;		//spear head nepali date...

            int total_eDays = 0; int total_nDays = 0; int a = 0; int day = 7 - 1;		//all the initializations...
            int m = 0; int y = 0; int i = 0; int j = 0;
            int numDay = 0;

            // count total no. of days in-terms of year
            for (int x = 0; x < (eDate.Year - def_eyy); x++)
            {	//total days for month calculation...(english)
                if (is_leap_year(def_eyy + x))
                    for (int w = 0; w < 12; w++)
                        total_eDays += lmonth[w];
                else
                    for (int z = 0; z < 12; z++)
                        total_eDays += month[z];
            }

            // count total no. of days in-terms of month					
            for (int p = 0; p < (eDate.Month - 1); p++)
            {
                if (is_leap_year(eDate.Year))
                    total_eDays += lmonth[p];
                else
                    total_eDays += month[p];
            }

            // count total no. of days in-terms of date
            total_eDays += eDate.Day;
            i = 0; j = def_nyy.Month;
            total_nDays = def_nyy.Day;
            m = def_nyy.Month;
            y = def_nyy.Year;

            // count nepali date from array
            while (total_eDays != 0)
            {
                a = ne.bs[i, j];
                total_nDays++;						//count the days
                day++;								//count the days interms of 7 days
                if (total_nDays > a)
                {
                    m++;
                    total_nDays = 1;
                    j++;
                }
                if (day > 7)
                    day = 1;
                if (m > 12)
                {
                    y++;
                    m = 1;
                }
                if (j > 12)
                {
                    j = 1; i++;
                }
                total_eDays--;
            }

            numDay = day;
            if (m.ToString().Length == 1)
            {
                nnDate = y + "/0" + m;
            }
            else
            {
                nnDate = y + "/" + m;
            }
            if (total_nDays.ToString().Length == 1)
            {
                nnDate = nnDate + "/0" + total_nDays;
            }
            else
            {
                nnDate = nnDate + "/" + total_nDays;
            }


        }
        return nnDate;
    }
    public string ConvertToN(string date)
    {
        string Convertdate = "";
        if (date != "")
        {
            Convertdate = ConvertEToN(DateTime.Parse(date));
        }
        else
        {
            Convertdate = "";
        }


        return Convertdate;
    }

    #region Nepalid Date || by ram babu

    public bool CheckNepaliDate(int[] a)
    {
        bool flag = false;
        try
        {
            Int32 a0 = a[0];
            Int32 a1 = a[1];
            Int32 a2 = a[2];
            Int32 j1 = a2 - 1977;
            Int32 ii0;
            Int32 ii213;
            if (a[0] < 1977 || a[0] > 2110)
            {
                ii0 = bs[j1, 0];
                ii213 = bs[j1, a0];// for month
                if (ii213 >= a1 && ii0 == a2 && a0 < 13)
                    flag = true;
                else
                    flag = false;
            }
        }
        catch (Exception ex)
        {
            flag = false;
        }

        return flag;
    }

    public bool IsValidNepaliDate(string nepaliDate)
    {
        bool isValidNepaliDate = false;

        string DATE_NEP = (nepaliDate);
        string[] nep_ = DATE_NEP.Split('/');
        int s1 = Convert.ToInt16(nep_[0]);
        int s2 = Convert.ToInt16(nep_[1]);
        int s3 = Convert.ToInt16(nep_[2]);
        int s4 = Convert.ToInt32(s2);
        int s5 = Convert.ToInt32(s3);
        int[] ii1 = { s2, s3, s1 };
        //isValidNepaliDate = nep.IsValidNepaliDate(NepaliDateFormat(nepaliDate));
        //ConvertNE ne = new ConvertNE();
        isValidNepaliDate = (CheckNepaliDate(ii1));
        return isValidNepaliDate;

    }

    public string NepaliDateFormat(string nepaliDate)
    {
        string dateNep = nepaliDate;
        string[] nep_ = dateNep.Split('/');
        string s1 = nep_[0];
        string s2 = nep_[1];
        string s3 = nep_[2];
        int s4 = Convert.ToInt32(s2);
        int s5 = Convert.ToInt32(s3);

        if (s2.Length == 1)
            s2 = "0" + s2;
        if (s3.Length == 1)
            s3 = "0" + s3;
        //DATE_NEP = s1 + "/" + s4 + "/" + s5;
        dateNep = s1 + "/" + s2 + "/" + s3;
        return dateNep;
    }

    public string GetMonthStartEndDate(string year, string month)
    {
        string date1 = string.Empty;
        string DATE_NEP = "";// (nepaliDate);
        string[] nep_ = DATE_NEP.Split('/');
        int s1 = Convert.ToInt16(nep_[0]);
        int s2 = Convert.ToInt16(nep_[1]);
        int s3 = Convert.ToInt16(nep_[2]);
        int s4 = Convert.ToInt32(s2);
        int s5 = Convert.ToInt32(s3);
        int[] ii1 = { s2, s3, s1 };
        // date1 = (GetNepaliDate(ii1));
        return date1;

    }

    public string GetNepaliMonthEndtDate(string year, string month)
    {
        string date1 = string.Empty;
        try
        {
            Int32 y = Int32.Parse(year);
            Int32 m = Int32.Parse(month);
            Int32 y1 = y - 2000;
            Int32 ii0 = 0;
            Int32 ii213;
            if (y < 2089)
            {
                ii0 = bs[y1, 0];
                ii213 = bs[y1, m];// for month
                //if (ii213 >= a1 && ii0 == a2 && a0 < 13)
                //    date1 = true;
                //else
                //    date1 = false;
                date1 = year + "/" + month + "/" + ii213.ToString();
            }
        }
        catch (Exception ex)
        {
            //  date1 = false;
        }

        return date1;
    }

    //public DateTime GetNepaliMonthEndtDate1(string year, string month)
    //{
    //    DateTime date1 = new DateTime();
    //    try
    //    {
    //        Int32 y = Int32.Parse(year);
    //        Int32 m = Int32.Parse(month);
    //        Int32 y1 = y - 2000;
    //        Int32 ii0 = 0;
    //        Int32 ii213;
    //        if (y < 2089)
    //        {
    //            ii0 = bs[y1, 0];
    //            ii213 = bs[y1, m];
    //            // date1 = year + "/" + month + "/" + ii213.ToString();
    //            date1 = DateTime.Parse(ConvertNE.ConvertNToE(DateStringToInt.StringToInt(year + "/" + month + "/" + ii213.ToString())));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //  date1 = false;
    //    }

    //    return date1;
    //}

    //public DateTime NepaliMonthEndtDate(string year, string month)
    //{
    //    DateTime date1 = new DateTime();
    //    try
    //    {
    //        Int32 y = Int32.Parse(year);
    //        Int32 m = Int32.Parse(month);
    //        Int32 y1 = y - 2000;
    //        Int32 ii0 = 0;
    //        Int32 ii213;
    //        if (y < 2089)
    //        {
    //            ii0 = bs[y1, 0];
    //            ii213 = bs[y1, m];
    //            // date1 = year + "/" + month + "/" + ii213.ToString();
    //            date1 = DateTime.Parse(ConvertNE.ConvertNToE(DateStringToInt.StringToInt(year + "/" + month + "/" + ii213.ToString())));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //  date1 = false;
    //    }

    //    return date1;
    //}

    #endregion

    public static string ConvertEToNWithDash(DateTime eDate)
    {
        string nnDate = "";
        ConvertNE ne = new ConvertNE();
        if (ne.is_range_eng(eDate))
        {
            // english month data.
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //DateTime def_eyy = new DateTime(1944,1,1);
            int def_eyy = 1921;
            //spear head english date...
            DateTime def_nyy = new DateTime(1977, 9, 18 - 1);
            //$def_nyy = 2000; $def_nmm = 9; $def_ndd = 17-1;		//spear head nepali date...

            int total_eDays = 0; int total_nDays = 0; int a = 0; int day = 7 - 1;		//all the initializations...
            int m = 0; int y = 0; int i = 0; int j = 0;
            int numDay = 0;

            // count total no. of days in-terms of year
            for (int x = 0; x < (eDate.Year - def_eyy); x++)
            {	//total days for month calculation...(english)
                if (is_leap_year(def_eyy + x))
                    for (int w = 0; w < 12; w++)
                        total_eDays += lmonth[w];
                else
                    for (int z = 0; z < 12; z++)
                        total_eDays += month[z];
            }

            // count total no. of days in-terms of month					
            for (int p = 0; p < (eDate.Month - 1); p++)
            {
                if (is_leap_year(eDate.Year))
                    total_eDays += lmonth[p];
                else
                    total_eDays += month[p];
            }

            // count total no. of days in-terms of date
            total_eDays += eDate.Day;
            i = 0; j = def_nyy.Month;
            total_nDays = def_nyy.Day;
            m = def_nyy.Month;
            y = def_nyy.Year;

            // count nepali date from array
            while (total_eDays != 0)
            {
                a = ne.bs[i, j];
                total_nDays++;						//count the days
                day++;								//count the days interms of 7 days
                if (total_nDays > a)
                {
                    m++;
                    total_nDays = 1;
                    j++;
                }
                if (day > 7)
                    day = 1;
                if (m > 12)
                {
                    y++;
                    m = 1;
                }
                if (j > 12)
                {
                    j = 1; i++;
                }
                total_eDays--;
            }

            numDay = day;
            if (m.ToString().Length == 1)
            {
                nnDate = y + "-0" + m;
            }
            else
            {
                nnDate = y + "-" + m;
            }
            if (total_nDays.ToString().Length == 1)
            {
                nnDate = nnDate + "-0" + total_nDays;
            }
            else
            {
                nnDate = nnDate + "-" + total_nDays;
            }


        }
        return nnDate;
    }


}



