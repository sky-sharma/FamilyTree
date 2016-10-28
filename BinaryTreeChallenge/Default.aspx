<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BinaryTreeChallenge.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        div
        {
            font-family :Courier New, Courier, monospace;
        }
        pre
        {
            display: inline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Welcome to Adam&#39;s Family Tree!&nbsp; Each Family member can have up to two children.<br />
        Add new family members as per the following rules:<br />
        <br />
        1.&nbsp; Pick the parent.<br />
        2.&nbsp; Enter a unique name for the child upto 11 characters long.<br />
        3.&nbsp; Click &quot;Add Child&quot;.<br />
        4.&nbsp; You will see the Family Tree updated.<br />
        <br />
        After the family tree has been set:<br />
        <br />
        1.&nbsp;Pick the names of two members from the Drop Downs.<br />
        2. Click &quot;Find Common Ancestor&quot;.<br />
        3. The common ancestor will be displayed.<br />
        <br />
        <br />
        List of All Family Members: <asp:DropDownList ID="familyMemberList" runat="server" Height="16px" Width="199px">
        </asp:DropDownList>
        <br />
        <br />
        Child Name:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="childNameTextBox" runat="server" Width="184px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="addChild" runat="server" OnClick="addChild_Click" Text="Add Child" />
        <br />
        <br />
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
        <br />
        <br />
        Family Tree:<br />
        <asp:Label ID="familyTreeLabel" runat="server"></asp:Label>
        <br />
        <br />
        First Family Member:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="firstFamilyMember" runat="server" Height="17px" Width="154px">
        </asp:DropDownList>
        <br />
        Second Family Member:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="secondFamilyMember" runat="server" Height="17px" Width="154px">
        </asp:DropDownList>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="findCommonAncestor" runat="server" OnClick="findCommonAncestor_Click" Text="Find Common Ancestor" Width="155px" />
&nbsp;
        <br />
        <br />
        Common Ancestor:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="commonAncestor" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
