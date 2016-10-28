using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BinaryTreeChallenge
{
    public partial class Default : System.Web.UI.Page
    {
        FamilyTree OurFamilyTree;

        // Stack to keep track of added child ancestors
        Stack<FamilyMember> addedChildAncestors;

        // Stacks to keep track of all possible ancestors of related family members
        Stack<FamilyMember> firstMemberAncestors;
        Stack<FamilyMember> secondMemberAncestors;

        // Keep track of the two related family members
        Stack<FamilyMember> related;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Create new Adam's Family Tree
                OurFamilyTree = new FamilyTree("Adam");

                // Create Stack for addedChildAncestors
                addedChildAncestors = new Stack<FamilyMember>();

                // Create Stack for firstMemberAncestors
                firstMemberAncestors = new Stack<FamilyMember>();

                // Create Stack for secondMemberAncestors
                secondMemberAncestors = new Stack<FamilyMember>();

                // Create Stack of two related family members
                related = new Stack<FamilyMember>();

                // Add Founder Name to Parent List
                familyMemberList.Items.Add(OurFamilyTree.FamilyFounder.Name);

                // Add FamilyTree to ViewState
                ViewState.Add("OurFamilyTree", OurFamilyTree);

                // Add Stack of ancestors to ViewState
                ViewState.Add("addedChildAncestors", addedChildAncestors);

                // Add Stack of firstMemberAncestors to ViewState
                ViewState.Add("firstMemberAncestors", firstMemberAncestors);

                // Add Stack of secondMemberAncestors to ViewState
                ViewState.Add("secondMemberAncestors", secondMemberAncestors);

                // Add Stack of related family members (2) to ViewState
                ViewState.Add("related", related);
            }
        }

        protected void addChild_Click(object sender, EventArgs e)
        {
            string newChildName = "";
            string newParentName = "";
            newChildName = childNameTextBox.Text;
            newParentName = familyMemberList.SelectedItem.Text;

            // Get FamilyTree and stacks of ancestors and related from ViewState
            OurFamilyTree = (FamilyTree)ViewState["OurFamilyTree"];
            addedChildAncestors = (Stack<FamilyMember>)ViewState["addedChildAncestors"];
            related = (Stack<FamilyMember>)ViewState["related"];

            FamilyMember ParentAboutToGetNewChild =
                OurFamilyTree.FindFamilyMemberByName(newParentName, ref addedChildAncestors);

            // string listOfAncestors = "<br/>";

            /*
            // Build long string of ancestors
            foreach (var ancestor in addedChildAncestors)
            {
                listOfAncestors += ancestor.Name + " ";
            }
            */

            resultLabel.Text = ParentAboutToGetNewChild.AddChild(newChildName);
            //    + "<br/>Ancestor Chain:" + listOfAncestors;
            familyTreeLabel.Text = OurFamilyTree.DisplayFamilyTree(ref addedChildAncestors, ref related, null);

            // Clear stack of ancestors
            addedChildAncestors.Clear();

            // Clear all dropdowns.
            familyMemberList.Items.Clear();
            firstFamilyMember.Items.Clear();
            secondFamilyMember.Items.Clear();

            foreach (var name in OurFamilyTree.ListOfAllNames)
            {
                familyMemberList.Items.Add(name.ToString());
                firstFamilyMember.Items.Add(name.ToString());
                secondFamilyMember.Items.Add(name.ToString());
            }
        }

        protected void findCommonAncestor_Click(object sender, EventArgs e)
        {
            FamilyMember firstCommonAncestor = null;

            // Get stuff from ViewState
            OurFamilyTree = (FamilyTree)ViewState["OurFamilyTree"];
            firstMemberAncestors = (Stack<FamilyMember>)ViewState["firstMemberAncestors"];
            secondMemberAncestors = (Stack<FamilyMember>)ViewState["secondMemberAncestors"];
            related = (Stack<FamilyMember>)ViewState["related"];

            // Create a stack to hold uncommon ancestors for both family members.
            Stack<FamilyMember> uncommonAncestors = new Stack<FamilyMember>();

            // Find first related family member by name.
            FamilyMember firstRelatedFamilyMember =
                OurFamilyTree.FindFamilyMemberByName(firstFamilyMember.SelectedValue, ref firstMemberAncestors);
            
            // Find second related family member by name.
            FamilyMember secondRelatedFamilyMember =
                OurFamilyTree.FindFamilyMemberByName(secondFamilyMember.SelectedValue, ref secondMemberAncestors);

            // Push both related family members on stack of related.
            related.Push(firstRelatedFamilyMember);
            related.Push(secondRelatedFamilyMember);

            foreach (var firstMemberAncestor in firstMemberAncestors)
            {
                // If the First Member Ancestor being investigated is not in the second Member Ancestors
                // then it is uncommon, so push it into the uncommonAncestorsStack.

                if (!secondMemberAncestors.Contains(firstMemberAncestor))
                    uncommonAncestors.Push(firstMemberAncestor); 
            }

            // Do the same as above reversing first and second member ancestors

            foreach (var secondMemberAncestor in secondMemberAncestors)
            {
                // If the Second Member Ancestor being investigated is not in the first Member Ancestors
                // then it is uncommon, so push it into the uncommonAncestorsStack.

                if (!firstMemberAncestors.Contains(secondMemberAncestor))
                    uncommonAncestors.Push(secondMemberAncestor);
            }

            // Find first common ancestor
            foreach (var firstMemberAncestor in firstMemberAncestors)
            {
                if (secondMemberAncestors.Contains(firstMemberAncestor))
                {
                    firstCommonAncestor = firstMemberAncestor;
                    break;
                }
            }

            familyTreeLabel.Text = OurFamilyTree.DisplayFamilyTree(ref uncommonAncestors, ref related, firstCommonAncestor);

            commonAncestor.Text = firstCommonAncestor.Name;

            firstMemberAncestors.Clear();
            secondMemberAncestors.Clear();
            uncommonAncestors.Clear();
        }
    }
}