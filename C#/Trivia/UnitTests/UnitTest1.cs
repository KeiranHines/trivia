using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UglyTrivia;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class GoldenMaster
    {
        [TestMethod]
        public void TestMethod()
        {
            bool notAWinner = false;

            var logger = new Mock<Logger>();
            logger.Setup(a => a.WriteLine(It.IsAny<string>())).Verifiable("not");
            var game = new Game(logger.Object);

            var names = new [] {"Chet","Pat","Sue" };
            int num = 1;
            foreach (var name in names)
            {
                game.add(name);
                logger.Verify(a => a.WriteLine($"{name} was added"));
                logger.Verify(a => a.WriteLine($"They are player number {num}"));
                num++;
            }
                
            Random rand = new Random(4);

            bool wrong = false;

            var locations = new Dictionary<string, int>();
            foreach (var name in names)
                locations.Add(name, 0);

            for (int i = 0; i < 20; i++)
            {
                foreach (var name in names)
                {

                    Debug.WriteLine($"Doing run {i} for {name}");

                    wrong = false;
                    if (i % 2 == 0) wrong = true;
                    int roll = rand.Next(5) + 1;
                    game.roll(roll);
                    locations[name] += roll;

                    Debug.WriteLine($"Wrong: {wrong}, Roll: {roll}");


                    if (wrong)
                        notAWinner = game.wrongAnswer();
                    else
                        notAWinner = game.wasCorrectlyAnswered();

                    logger.Verify(a => a.WriteLine($"{name} is the current player"));
                    logger.Verify(a => a.WriteLine($"They have rolled a {roll}"));
                    logger.Verify(a => a.WriteLine($"{name}'s new location is {locations[name]}"));



                    if (wrong)
                    {
                        //verify we output wrong lines

                    }
                    else
                    {
                        //verify correct

                    }
                }
            }
                





            logger.Verify(a => a.WriteLine(It.IsAny<string>()));

        }
    }
}
