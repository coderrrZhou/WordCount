using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zg2
{
    class Program
    {
        //程序入口
        static void Main(string[] args)
        {
            //实例化Main主体
            Main main = new Main();

            //文件路径
            String path = "";//读取路径
            String path2 = "";//输出路径
            
            
            //读取命令行参数
            var arguments = CommandLineArgumentParser.Parse(args);
            
            
            //生成文件的存储路径（写入） -o String
            if (arguments.Has("-o"))
            {
                path2 = arguments.Get("-o").Next;
            }


            //读取文件路径 -i String
            if (arguments.Has("-i"))
            {
                path = arguments.Get("-i").Next;
                //打开文件,并读取
                main.Open_file(path);
                main.output(path2);
            }
            
            //获取给定长度的词组 -m number
            if (arguments.Has("-m"))
            {
                int number = Convert.ToInt32(arguments.Get("-m").Next);
                main.Read('m', number, path2);
            }

            //输出前n多的单词 -n number
            if (arguments.Has("-n"))
            {
                int number = Convert.ToInt32(arguments.Get("-n").Next);
                main.Read('n', number, path2);
            }
            
        }
    }
}
