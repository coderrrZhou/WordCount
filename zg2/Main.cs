using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zg2
{
    class Main
    {
        public Hashtable word;//单词表
        public Hashtable words;//单词词组
        ArrayList akeys;//辅助排序、输出单词集合

        string[] lines;//文件内容
        cal_words cal = new cal_words();//声明cal_words实例
        int num_lines = 0;//行数
        int num_ch = 0;//字符数
        int num_words = 0;//单词数

        public Main() { }

        //打开文件路径
        public void Open_file(String path)
        {
            lines = System.IO.File.ReadAllLines(@path);
            word = new Hashtable();//置空
            words = new Hashtable();//置空
            num_lines = 0;//行数 置空
            num_ch = 0;//字符数 置空
            num_words = 0;//单词数 置空
        }

        //读取文件中的单词，并统计
        //此处path参数为输出文件路径
        public void Read(char param, int num,String path)
        {
            switch (param)
            {
                case 'm':
                    foreach (String s in lines)
                        cal.cal_specifide(num, s, words);
                    akeys = new ArrayList(words.Keys);
                    cal.output(akeys, words,path);
                    break;

                case 'n':
                    cal.sort(akeys, word);
                    cal.out_pre(num, word,path);
                    break;

                default:
                    Console.WriteLine("wrong parameter!!!");
                    break;
            }
        }

        //常规数据输出 -----字符数、单词数、行数
        public void output(String path)
        {
            foreach (string line in lines)
            {
                num_ch += cal.cal_ch(line);//计算字符数（可以移植到单词数cal()中），待定
                if (line.Equals(""))
                    continue;
                else
                    num_words += cal.cal(line, word);//计算单词数
                num_lines++;//计算行数
            }
            if (path.Equals(""))
            {
                Console.WriteLine("\tCharacter : "+num_ch);
                Console.WriteLine("\tWords : " + num_words);
                Console.WriteLine("\tLines : " + num_lines);
            }
            else
            {
                //因为常规数据输出为第一次文件写入，所以将其覆盖，设置开关为false
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path, false))
                {
                    file.WriteLine("\tCharacter : " + num_ch);// 直接追加文件末尾，换行 
                    file.WriteLine("\tWords : " + num_words);
                    file.WriteLine("\tLines : " + num_lines);
                }
            }
        }
    }
}
