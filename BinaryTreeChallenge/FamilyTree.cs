using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace BinaryTreeChallenge
{
    [Serializable]
    public class FamilyTree
    {
        public FamilyMember FamilyFounder { get; set; }

        public Queue<string> ListOfAllNames { get; set; }

        // How wide is each column in the displayed Family Tree.
        const int ColumnLength = 11;
        const int ColumnCorrection = 2;

        public FamilyTree(string FamilyFounderName)
        {
            // Create Founding Family Member, assign this Family Tree to
            // him /her and add his/her name to ListOfAllNames
            FamilyFounder = new FamilyMember(FamilyFounderName, this);
            ListOfAllNames = new Queue<string>();
            ListOfAllNames.Enqueue(FamilyFounderName);
        }
        
        public FamilyMember FindFamilyMemberByName(string FamilyMemberName, ref Stack<FamilyMember> ancestors)
        {
            // Push the founder on the stack of ancestors
            ancestors.Push(FamilyFounder);

            // Call recursive routine to find family member
            FamilyMember _foundFamilyMember =
                _findFamilyMemberByName(FamilyFounder, FamilyMemberName, ref ancestors);

            return (_foundFamilyMember);
        }

        private FamilyMember _findFamilyMemberByName(FamilyMember _familyMemberToCheck,
            string _familyMemberName, ref Stack<FamilyMember> ancestors)
        {
            FamilyMember _foundFamilyMember = null;

            // If the name being searched for matches the name of this family member,
            // then return this family member
            if (_familyMemberToCheck.Name == _familyMemberName)
                return (_familyMemberToCheck);

            // If the name being searched for did NOT match the name of this family member
            // then search hierarchically among all first children
            if ((_foundFamilyMember == null) && (_familyMemberToCheck.FirstChild != null))
            {
                // Push the current potential ancestor on the stack
                ancestors.Push(_familyMemberToCheck.FirstChild);

                _foundFamilyMember = _findFamilyMemberByName(_familyMemberToCheck.FirstChild,
                        _familyMemberName, ref ancestors);
            }

            /*
            // Pull the last potential ancestor off the stack
            ancestors.Pop();
            */

            // If the name being searched for did NOT match the name of this family member
            // then search hierarchically among all second children
            if ((_foundFamilyMember == null) && (_familyMemberToCheck.SecondChild != null))
            {
                // Push the current potential ancestor on the stack
                ancestors.Push(_familyMemberToCheck.SecondChild);

                _foundFamilyMember = _findFamilyMemberByName(_familyMemberToCheck.SecondChild,
                    _familyMemberName, ref ancestors);
            }

            /*
            // Pull the last potential ancestor off the stack.  That one
            // cannot be the ancestor since we returned from this recursive call.
            ancestors.Pop();
            */

            // If _foundFamilyMember was null then this was not a potential
            // ancestor so remove.
            if (_foundFamilyMember == null) ancestors.Pop();

            // Return _foundFamilyMember.
            return _foundFamilyMember;
        }

        public string DisplayFamilyTree(ref Stack<FamilyMember> ancestors, ref Stack<FamilyMember> related,
            FamilyMember firstCommonAncestor)
        {
            string FamilyTreeString = "";

            // We always have at least one generation
            int GenerationCounter = 1;

            _displayFamilyTree(FamilyFounder, ref FamilyTreeString,
                ref GenerationCounter, ref ancestors, ref related, firstCommonAncestor);
            return FamilyTreeString;
        }

        private void _displayFamilyTree(FamilyMember _familyMemberToDisplay,
            ref string FamilyTreeString, ref int GenerationCounter,
            ref Stack<FamilyMember> ancestors, ref Stack<FamilyMember> related,
            FamilyMember firstCommonAncestor)
        {
            string verticalBranch = "↑";
            string fontColor = "\"black\"";
            string familyMemberName = _familyMemberToDisplay.Name;
            string formattedFamilyMemberName = familyMemberName.PadLeft(ColumnLength, '-');

            // If the _familyMemberToDisplay is an ancestor of the recently added child
            // then change the color to blue.
            if (ancestors.Contains(_familyMemberToDisplay)) fontColor = "\"blue\"";

            // If the _familyMemberToDisplay is one of the two members we are trying to find the common
            // ancestor of, then override the color to green
            if (related.Contains(_familyMemberToDisplay)) fontColor = "\"green\"";

            // If the _familyMemberToDisplay is the first common
            // ancestor, then override the color to red
            if (_familyMemberToDisplay == firstCommonAncestor) fontColor = "\"red\"";

            formattedFamilyMemberName = "<font color = " + fontColor + ">" + formattedFamilyMemberName + "</font>";

            FamilyTreeString += formattedFamilyMemberName;

            if (_familyMemberToDisplay.FirstChild != null)
            {
                // Increment GenerationCounter before entering recursive instance.
                GenerationCounter++;

                _displayFamilyTree(_familyMemberToDisplay.FirstChild,
                    ref FamilyTreeString, ref GenerationCounter, ref ancestors, ref related, firstCommonAncestor);

                // Decremenet GenerationCounter after exiting recursive instance.
                GenerationCounter--;
            }

            if (_familyMemberToDisplay.SecondChild != null)
            {

                string lineBreaks = "<br/>";

                /*
                // (NumberOfGenerations - GenerationCounter) defines how many rows
                // down to go to display the family member
                for (int i = 0; i < (MaxNumGenerations - GenerationCounter); i++)
                {
                    lineBreaks += "<br/>";
                }
                */
                // GenerationCounter++;

                // Prepend current row of FamilyTreeString with the following:
                // 1. Next line.
                // 2. As many columns of white space as defined by DisplayColumn
                // (equivalent to number of levels or recursion)
                // 3. Vertical branch (pipe character)
                // NOTE: the <pre> after <br/> was necessary because browsers ignore leading
                // multiple-white spaces. Closed with </pre> further on.
                // ALSO NOTE: Don't know why the ColumnCorrection was needed after ColumnLength,
                // but without it the results didn't look aligned.

                FamilyTreeString += lineBreaks + "<pre>" +
                    verticalBranch.PadLeft(((ColumnLength + ColumnCorrection) * GenerationCounter), ' ');

                FamilyTreeString += "</pre>";

                // Increment GenerationCounter before entering recursive instance.
                GenerationCounter++;

                _displayFamilyTree(_familyMemberToDisplay.SecondChild,
                    ref FamilyTreeString, ref GenerationCounter, ref ancestors, ref related, firstCommonAncestor);

                // Decremenet GenerationCounter after exiting recursive instance.
                GenerationCounter--;
            }
        }

        /*
        public string FindCommonAncestor(string firstFamilyMemberName, string secondFamilyMemberName, ref Queue<FamilyMember> ancestors)
        {
            // If names are same, this is the same person, so common ancestor is self.
            if (firstFamilyMemberName == secondFamilyMemberName) return (firstFamilyMemberName);
            _findAllAncestorsOfFirstMember(firstFamilyMemberName, ref ancestors, FamilyFounder);
        }

        private string _findAllAncestorsOfFirstMember(string firstFamilyMemberName, ref Queue<FamilyMember> ancestors, FamilyMember familyMemberToCheck)
        {

        }
        */

        public string AddNameToList(string _newFamilyMemberName)
        {
            if (ListOfAllNames.Contains(_newFamilyMemberName))
                return String.Format("Could not add {0}. That member is already in the list.", _newFamilyMemberName);
            else
            {
                ListOfAllNames.Enqueue(_newFamilyMemberName);
                return String.Format("Added {0} to list of family members.", _newFamilyMemberName);
            }
        }
    }
}