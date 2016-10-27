using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Linq;

namespace RegExp
{
    class Program
    {

      
        
        static void Main(string[] args){
            
            
            string str="Someinformation, JustSome, JustInfroamtion, OrJustSomeInformation,DoubleInformationInformation";
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Program SomeObj = new Program();
      Task<string> WordTask=new Task<string>(()=>  SomeObj.MostFrequentTriplet(str,token));
      WordTask.Start();
      Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения:");
      string s = Console.ReadLine();
      if (s == "Y")
          cancelTokenSource.Cancel();

      else
      {
          Console.WriteLine(WordTask.Result);
          Console.ReadLine();

      }
        }
       

        
        string MostFrequentTriplet(string str,CancellationToken token)
        {
            string FunctionResult="";
          


            if (!token.IsCancellationRequested)
            {
                Console.WriteLine();

                List<string> words = new List<string>();
                string[] SomeStringArray = str.Split(',');
                
                for (int i = 0; i < SomeStringArray.Length; i++)
                {   
                    for (int j = 0; j < SomeStringArray[i].Length; j++)
                    {
                        try
                        {
                            words.Add(SomeStringArray[i][j].ToString() + SomeStringArray[i][j + 1].ToString() + SomeStringArray[i][j + 2].ToString());
                        }

                        catch
                        {
                            break;
                        }
                    }



                }

                var maxCount = words.Max(i => i.LongCount());
                var result = words
                 .Select(strg => new { Name = strg, Count = words.Count(s2 => s2 == strg) })
                 .Where(obj => obj.Count == maxCount)
                 .Distinct()
                 .ToDictionary(obj => obj.Name, obj => obj.Count);

                int k=1;
                
                foreach (var a in result)
                {
                    if (result.Count == k)

                        FunctionResult +=  a.Key + "\t" + a.Value;

                    else
                    {
                        FunctionResult +=a.Key + "\t" + a.Value + ", ";
                        k = k + 1;
                    }
                }
                return FunctionResult;

            }
            else
            {
                Console.WriteLine("Операция прервана");
                return "";
            }

           
                    
        }

    }
    
}
