using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            
            double[] science = new double[stdCount];
            double[] math = new double[stdCount];
            double[] english = new double[stdCount];
            double totalScore = 0;

            for (int i = 1; i <= stdCount; i++){
                math[i - 1] = double.Parse(data[i, 2]);
                science[i - 1] = double.Parse(data[i, 3]);
                english[i - 1] = double.Parse(data[i, 4]);
            }

            var studentScores = new (string Name, double TotalScore)[stdCount];
          
            for (int i = 1; i <= stdCount; i++){
                totalScore = math[i - 1] + science[i - 1] + english[i - 1];
                studentScores[i - 1] = (data[i, 1], totalScore);
            }
            
            
            int[] ranks = new int[stdCount];

            for (int i = 0; i < stdCount; i++){
                int rank = 1;
                for (int j = 0; j < stdCount; j++){
                    if (studentScores[i].TotalScore < studentScores[j].TotalScore) {
                        rank++;
                    }
                }
                ranks[i] = rank;
            }

            Console.WriteLine("Average Scores: ");
            Console.WriteLine($"Math: {math.Average():F2}");
            Console.WriteLine($"Science: {science.Average():F2}");
            Console.WriteLine($"English: {english.Average():F2}");
            Console.WriteLine();
            Console.WriteLine("Max and min Scores: ");
            Console.WriteLine($"Math: ({math.Max()}, { math.Min()})");
            Console.WriteLine($"Science: ({science.Max()}, {science.Min()})");
            Console.WriteLine($"English: ({english.Max()}, {english.Min()})");
            Console.WriteLine();
            Console.WriteLine("Students rank by total scores:");
            for (int i = 0; i < stdCount; i++)
            {
                Console.WriteLine($"{studentScores[i].Name}: {ranks[i]}th");
            }
            
            // --------------------
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/
