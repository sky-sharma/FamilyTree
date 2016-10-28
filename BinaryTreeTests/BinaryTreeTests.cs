using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BinaryTreeChallenge;

namespace BinaryTreeTests
{
    [TestFixture]
    public class AddChildTests
    {
        FamilyTree AddamsFamily = new FamilyTree("Adam");

        [Test]
        public void ShouldNotAcceptBlankChildName()
        {
            string expectedResult = "Child name cannot be blank.";
            string runResult = AddamsFamily.FamilyFounder.AddChild("");
            Assert.That(runResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShouldNotAcceptDuplicateName()
        {
            string expectedResult = "New child was not added.  " +
                "The name Adam is already in the list.";
            string runResult = AddamsFamily.FamilyFounder.AddChild("Adam");
            Assert.That(runResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShouldAddOnlyTwoChildren()
        {
            string expectedResult = "Cain is the first child of Adam. " +
                "Abel is the second child of Adam. " + "New child was not added. " +
                " The parent already has two children.";
            string firstChildResult = AddamsFamily.FamilyFounder.AddChild("Cain");
            string secondChildResult = AddamsFamily.FamilyFounder.AddChild("Abel");
            string thirdChildResult = AddamsFamily.FamilyFounder.AddChild("Larry");
            Assert.That((firstChildResult + " " + secondChildResult + " " + thirdChildResult),
                Is.EqualTo(expectedResult));
        }
    }
}
