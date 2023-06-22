
using System.Linq.Expressions;
using System.Text.RegularExpressions;

string filePath = "F:\\FUFIRKA\\DZ_5\\DZ_5\\text1.txt";

if (File.Exists(filePath))
{
    using (StreamReader sr = new StreamReader(filePath))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}
else
{
    Console.WriteLine("Файл не найден");
}
Console.WriteLine();

int menu = 1; 
do
{
    Console.WriteLine("Выберите ДЗ " +
                      "\n 1. Поиск слова, содержащие максимальное колличество цифр " +
                      "\n 2. Поиск самого длинного слова и определить,сколько раз оно встречается в тексте " +
                      "\n 3. Заменить цифры на слова " +
                      "\n 4. Вопросительные  и восклицательные предложения " +
                      "\n 5. Вывести предложения без запятых" +
                      "\n 6. Найти слова, которые начинаются и заканчиваются на одну и туже буквы");
    Console.Write("Ваш выбор: ");
    string golos = Console.ReadLine();
    Console.WriteLine();
    string MaxWord = "" ;
    int MaxCount = 0;

    switch (golos)
    {
        case "1":

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection matches = Regex.Matches(line, @"\w*\d\w*"); 
                    foreach (Match match in matches)
                    {
                        string word = match.Value;
                        int digitsCount = Regex.Matches(word, @"\d").Count; 
                        if (digitsCount > MaxCount)
                        {
                            MaxWord = word;
                            MaxCount = digitsCount;
                        }
                    }

                }
            }
            if (MaxCount > 0)
            {
                Console.WriteLine($"Слово с максимальным количеством цифр: {MaxCount}");
            }
            else
            {
                Console.WriteLine("В файле нет слов, содержащих цифры");
            }
            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            break;
        case "2":
            string longestWord = "";
            int longestCount = 0;
            Dictionary<string,int> WordCount = new Dictionary<string,int>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');

                    foreach (string word in words)
                    {
                        if (word.Length > 0)
                        {
                            if(WordCount.ContainsKey(word))
                            {
                                WordCount[word]++;

                            }

                            else
                            {
                                WordCount.Add(word, 1);
                            }

                            if (word.Length > longestWord.Length)
                            {
                                longestWord = word;
                                longestCount = WordCount[word];
                            }

                            if (word.Length == longestWord.Length && WordCount[word] > longestCount)
                            {
                                longestWord = word;
                                longestCount = WordCount[word];
                            }
                        }
                    }

                }

                Console.WriteLine($"Самое длинное слово: {longestWord}, встретилось {longestCount} раз ");

            }
            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            break;

        case "3":
            Dictionary<char, string> words2 = new Dictionary<char, string>();
            words2.Add('0', "ноль ");
            words2.Add('1', "один ");
            words2.Add('2', "два ");
            words2.Add('3', "три ");
            words2.Add('4', "четыре ");
            words2.Add('5', "пять ");
            words2.Add('6', "шесть ");
            words2.Add('7', "семь ");
            words2.Add('8', "восемь ");
            words2.Add('9', "девять ");
            Regex regex = new Regex("[0-9]");

            using (StreamReader sr = new StreamReader(filePath)) 
             {
                 string line = sr.ReadToEnd();

                  line = regex.Replace(line, m => words2[m.Value[0]]);

                  Console.WriteLine(line);
            }
            
            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            break;
        case "4":
            Regex regex2 = new Regex(@"([^\.\?!]*[?|!])");
            List<string> qList = new List<string>();
            List< string> eList = new List< string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadToEnd();
                foreach (Match match in regex2.Matches(line))
                {
                    string sent = match.Value.Trim();
                    if (sent.EndsWith("?"))
                    {
                        qList.Add(sent);
                    }

                    if (sent.EndsWith("!"))
                    {
                        eList.Add(sent);
                    }
                    
                }

                Console.WriteLine("Вопросительные предложения: ");

                foreach (string q in qList)
                {
                    Console.WriteLine(q);
                }
                Console.WriteLine();
                Console.WriteLine("Восклицательные предложения: ");

                foreach (string e in eList )
                {
                    Console.WriteLine(e);

                }
                Console.WriteLine();

            }

            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            break;

        case "5":
            Regex regex_comma = new Regex(@"([^\.\?!]*[^,][\.\?!])");

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadToEnd();
                foreach (Match m in regex_comma.Matches(line) )
                {
                    string sent = m.Value.Trim();
                    Console.WriteLine(sent);
                    Console.WriteLine();

                }
                
            }
            Console.WriteLine();
            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            
            break;
        case "6":
            Regex regex_char = new Regex(@"\b([a-zA-Z\d])\w*\1\b");
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadToEnd();
                foreach (Match m in regex_char.Matches(line) )
                {
                    string word = m.Value;
                    Console.WriteLine(word);
                    Console.WriteLine();

                }
            }
            Console.WriteLine();
            Console.WriteLine("Возврат в меню? \n 1. Да \n 2. Нет");
            Console.Write("Ваш выбор: ");
            menu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            break;


    }
} while (menu == 1);
