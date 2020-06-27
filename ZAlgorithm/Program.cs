using System;

namespace ZAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "aabxaabxcaabxaabxay";
            string t = "xay";
            Program p = new Program();

            Console.WriteLine("ZAlgorithm");
            if(p.CheckSubString(s, t) == true)
            {                
                Console.WriteLine("Yes");
            }
            else Console.WriteLine("No");            
        }

        bool CheckSubString(string s, string t)
        {
            string com = t + '$' + s;

            int[] zArray = new int[com.Length];

            ZAlgorithm(com, zArray);

            for(int i = 0; i<zArray.Length; i++)
            {
                if(zArray[i] == t.Length)
                return true;
            }

            return false;
        }

        void ZAlgorithm(string com, int[] zArray)
        {
            int itr = 1;
            zArray[0] = 0;

            while(itr<zArray.Length)
            {
                int matches = 0;
                int temp = itr;
                int l = 0;
                while(l<com.Length && temp<com.Length && com[l] == com[temp])
                {
                    l++;
                    temp++;
                    matches++;
                }

                zArray[itr] = matches;

                if(matches > 1)
                {
                    int start = itr + 1;
                    int end = temp - 1;
                    int prefix = 1;
                    while(end<com.Length && start<com.Length && prefix<com.Length && start<=end && com[start] == com[prefix])
                    {
                        if(zArray[prefix] + start <= end)
                        {
                            zArray[start] = zArray[prefix];
                            start++;
                            prefix++;
                        }
                        else{
                            int origin = prefix;
                            int soFar = zArray[prefix];
                            prefix++;
                            end++;
                            while(end<com.Length && prefix<com.Length && com[end] == com[prefix])
                            {
                                soFar++;
                                end++;
                                prefix++;
                            }

                            zArray[start] = soFar;
                            start++;
                            end--;
                            prefix = origin + 1;
                        }
                    }

                    itr = start;                    
                }
                else itr++;
            }
        }
    }
}
