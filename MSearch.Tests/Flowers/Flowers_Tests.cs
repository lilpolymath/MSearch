﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSearch.Extensions;
using MSearch.Tests.Common;
using MSearch.Flowers;
using MSearch.Tests.Problems.Sphere;
using MSearch.Tests.Problems.Knapsacks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSearch.Tests.Flowers
{
    [TestClass]
    public class Flowers_Tests
    {
        [TestMethod]
        public void Test_That_Flower_Pollination_On_Sphere_Works()
        {
            Pollination<double> garden = new Pollination<double>();
            SphereList sphere = new SphereList();
            garden.create(sphere.getConfiguration());
            garden.fullIteration();
        }

        [TestMethod]
        public void Test_That_Flower_Pollination_On_Binary_Knapsack_Works()
        {
            // Pollination<double[]> garden = new Pollination<double[]>();
            // BinaryKnapsack bKnapsack = new BinaryKnapsack();
            // bKnapsack.Load($"data/knapsacks/json/mknapcb1/mknapcb1-1.json");
            // garden.create(bKnapsack.getConfiguration(), 0.4);
            // garden.fullIteration();
        }
    }
}
