using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace BinaryTreeChallenge
{
    [Serializable]
    public class FamilyMember
    {
        public string Name { get; set; }

        public FamilyMember FirstChild { get; set; }

        public FamilyMember SecondChild { get; set; }

        public FamilyTree FamilyTree { get; set; }

        public FamilyMember(string familyMemberName, FamilyTree familyTree)
        {
            Name = familyMemberName;
            FamilyTree = familyTree;
        }

        public string AddChild(string childName)
        {
            // Only add child if child's name is unique (i.e. not already in list).
            // Only two children can be added.

            // If the name is blank, return a message saying that is not allowed.
            if (childName == "")
                return "Child name cannot be blank.";

            // If the name is already in the list, don't add new family member.
            if (FamilyTree.ListOfAllNames.Contains(childName))
                return String.Format("New child was not added.  The name {0} is already in the list.",
                    childName);

            // If FirstChild AND SecondChild contain actual Family members
            // then this parent already has two children. So don't add this one.
            if (FirstChild != null && SecondChild != null)
                return ("New child was not added.  The parent already has two children.");
            else
            {
                FamilyMember newChild = new FamilyMember(childName, FamilyTree);
                FamilyTree.ListOfAllNames.Enqueue(childName);

                if (FirstChild == null)
                {
                    FirstChild = newChild;
                    return String.Format("{0} is the first child of {1}.", childName, Name);
                }
                else
                {
                    SecondChild = newChild;
                    return String.Format("{0} is the second child of {1}.", childName, Name);
                }
            }
        }
    }
}