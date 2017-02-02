﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSearch.Extensions;
using MSearch.Tests.Common;
using MSearch.Tests.Helpers.IO;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSearch.Tests.Problems
{
    [TestClass]
    public class Knapsack_Tests
    {
        private void saveKnapsacksToFile(List<Knapsack> knapsacks, string folderName, List<Knapsack_Problem.BestResult> results)
        {
            Directory.Create("~/data");
            Directory.Create("~/data/knapsacks");
            Directory.Create("~/data/knapsacks/json");
            Directory.Create($"~/data/knapsacks/json/{folderName}");
            for (int i = 0; i < knapsacks.Count; i++)
            {
                var knapsack = knapsacks[i];
                knapsack.goal = results.First(result => result.filename == folderName && result.index == (i + 1)).best;
                knapsack.ToJson().SaveToFile($"data/knapsacks/json/{folderName}/{folderName}-{i + 1}.json");
            }
        }

        private List<Knapsack_Problem.BestResult> getBestResults()
        {
            return JsonConvert.DeserializeObject<List<Knapsack_Problem.BestResult>>(System.IO.File.ReadAllText($"data/knapsacks/json/best-results.json"));
        }

        //[TestMethod]
        public void Test_That_Read_Problem_Works()
        {
            var results = getBestResults();
            for (int i = 1; i <= 9; i++)
            {
                Knapsack_Problem knapsackProblem = new Knapsack_Problem();
                List<Knapsack> ret = knapsackProblem.readProblem($"data/knapsacks/mknapcb{i}.txt");
                Console.WriteLine($"No of Knapsacks: {ret.Count}");
                Assert.AreEqual(ret.Count, 30, "No. of Knapsacks must equal 30");
                Console.WriteLine(ret.ToJson(true));
                saveKnapsacksToFile(ret, $"mknapcb{i}", results);
            }
        }

        //[TestMethod]
        public void Test_That_Knapsack_Load_Works()
        {
            Knapsack knapsack = new Knapsack();
            knapsack.Load("data/knapsacks/json/mknapcb1/mknapcb1-1.json");
            Assert.AreNotEqual(knapsack.weights.Count, 0);
            Console.WriteLine(knapsack.ToJson(true));
        }

        [TestMethod]
        public void Test_That_Knapsack_Get_Initial_Solution_Works()
        {
            Knapsack knapsack = new Knapsack();
            for (int i = 1; i <= 5; i++)
            {
                knapsack.Load($"data/knapsacks/json/mknapcb1/mknapcb1-{i}.json");
                for (int length = 1; length <= 5; length++)
                {
                    List<int> sol = knapsack.getInitialSolution(length);
                    Console.WriteLine($"{i}\tSolution: " + sol.ToJson());
                    Assert.AreEqual(sol.Count, length, "solution length must be " + length);
                    double fitness = knapsack.getFitness(sol);
                    Console.WriteLine($"{i}\tFitness: " + fitness);
                }
            }
        }
    }
}
