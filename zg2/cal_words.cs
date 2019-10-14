using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace zg2
{
    class cal_words
    {

        public cal_words(){}

        //统计单词数
        public int cal(String s, Hashtable word)
        {
            int num_words = 0;
            if (s.Length >= 4)
            {
                for (int i = 0; i < s.Length - 3;)
                {
                    if (!IsOnlyWord(s.Substring(i, 4)))
                    {
                        i++; continue;
                    }

                    for (int j = i + 3; j < s.Length; j++)
                    {
                        if (IsNumberAndWord(s[j] + "") && j == s.Length - 1)
                        {
                            num_words++;
                            addString(s.Substring(i, j - i + 1),word);
                            return num_words;
                        }
                        else if (!IsNumberAndWord(s[j] + ""))
                        {
                            num_words++;
                            addString(s.Substring(i, j - i),word);
                            i = j + 1;
                            break;
                        }
                    }
                }
            }
            return num_words;
        }

        //只有字母，用于统计单词的辅助函数
        private static bool IsOnlyWord(string value)
        {
            Regex r = new Regex(@"^[a-zA-Z]+$");
            return r.Match(value).Success;
        }

        //只有数字和字母，用于统计单词的辅助函数
        private static bool IsNumberAndWord(string value)
        {
            Regex r = new Regex(@"^[a-zA-Z0-9]+$");
            return r.Match(value).Success;
        }

        //统计字符数
        public int cal_ch(String s)
        {
            int nums_ch = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= 32 && s[i] <= 126)
                    nums_ch++;
            }
            return nums_ch;
        }
        
        //计算指定长度的单词词组
        public void cal_specifide(int length,String s,Hashtable words)
        {
            String[] temp = s.Split(new char[3] { ',', '.',' ' });
            for(int i = 0; i < temp.Length - 1; i++)
            {
                //一个词组应该处于同一个完整的句子中
                if (temp[i].Equals(".") || temp[i].Equals(",")) continue;

                bool isWords = true;
                String tmp_string = "";
                for (int j = i; j < i + length; j++)
                {
                    if (temp[j].Length < 4)
                    {
                        isWords = false;
                        break;
                    }
                    else if(!IsOnlyWord(temp[j].Substring(0, 4)))
                    {
                        isWords = false;
                        break;
                    }
                    else if (temp[j].Length>4 && !IsNumberAndWord(temp[j].Substring(4, temp[j].Length - 4)))
                    {
                        isWords = false;
                        break;
                    }
                    else tmp_string += " "+temp[j];
                }
                if (isWords)
                    addString(tmp_string, words);
                
            }
        }
       
        //将单词加入Hashtable中
        public void addString(String temp,Hashtable ht)
        {
            temp = temp.ToLower();

            foreach (DictionaryEntry de in ht)
            {
                if (de.Key.Equals(temp))
                {
                    ht[temp] = (int)de.Value + 1;
                    return;
                }
            }
            ht.Add(temp, 1);
        }


        //按照key排序
        //ArrayList akeys在此处初始化为word-key集合（Hashtable)
        public void sort(ArrayList akeys,Hashtable ht)
        {
            akeys = new ArrayList(ht.Keys);
            akeys.Sort(); //按字母顺序进行排序
        }

        //output输出
        public void output(ArrayList akeys,Hashtable ht,String path)
        {
            int n_word = 0;
   
            //控制台输出
            if (path.Equals(""))
            {
                Console.WriteLine("\n---------------输出分割线-------------------\n");
                foreach (string skey in akeys)
                {
                    n_word++;
                    Console.Write("    <" + skey + ">" + " : ");
                    Console.WriteLine(ht[skey]); 
                }
                Console.WriteLine("Total words : " + n_word);
            }
            //写入到文件中去
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path, true))
                {
                    file.WriteLine("\n---------------输出分割线-------------------");// 直接追加文件末尾，换行 
                    foreach (string skey in akeys)
                    {
                        n_word++;
                        string strTxt = "    <" + skey + ">" + " : " + ht[skey];
                        file.WriteLine(strTxt);
                    }
                    file.WriteLine("Total words : " + n_word);// 直接追加文件末尾，换行 
                }
            }
            
        }
        

        //自定义输出--》数量前n多的单词
        public void out_pre(int num,Hashtable ht,String path)
        {
            Console.WriteLine("以下是单词数量前 " + num + " 的单词：");
            //先对Hashtable中的value排序
            String[] keyArray = new String[ht.Count];
            int[] valArray = new int[ht.Count];

            ht.Keys.CopyTo(keyArray,0);
            ht.Values.CopyTo(valArray,0);

            Array.Sort(valArray, keyArray);
            Array.Reverse(keyArray);

            //若文件路径为空，则控制台输出
            if (path.Equals(""))
            {
                Console.WriteLine("\n---------------输出分割线-------------------\n");
                foreach (string skey in keyArray)
                {
                    num--;
                    Console.Write("    <" + skey + ">" + " : ");
                    Console.WriteLine(ht[skey] + "个"); //排序后输出
                    if (num == 0) return;
                }
            }
            //否则输出到文件中取
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path, true))
                {
                    file.WriteLine("\n---------------输出分割线-------------------");// 直接追加文件末尾，换行 
                    foreach (string skey in keyArray)
                    {
                        num--;
                        string strTxt = "    <" + skey + ">" + " : " + ht[skey] + "个";
                        file.WriteLine(strTxt);
                        if (num == 0) return;
                    }
                }
            }
            
        }

    }
}
