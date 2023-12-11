<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminCategory.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Manage Categories</h1>

   <!-- Displays all categories from the database--> 
    <asp:GridView ID="AdminCategoriesGridView" 
        AutoGenerateColumns="False" EmptyDataText="No data" AllowPaging="True"
        DataKeyNames="id" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="AdminCategoriesGridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="false" ReadOnly="true" SortExpression="id"/> 
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" /> 
        </Columns>
    </asp:GridView>
    
    <asp:Button ID="ButtonNewCategory" Text="New Category" runat="server" OnClick="NewCategory" />
    <!-- This panel is hidden until the user chooses to modify an entry -->
    <asp:Panel ID="PanelCategoryValues" Visible="false" runat="server">
        <asp:Label Text="Name" runat="server" />
        <asp:TextBox ID="TextBoxCategoryName" runat="server" />
        <br />

        <!-- Buttons for modifying the table-->
        <asp:Button ID="ButtonCreateCategory" Text="Create Category" Visible="false" runat="server" OnClick="CreateCategory" />
        <asp:Button ID="ButtonUpdateCategory" Text="Update Category" Visible="false" runat="server" OnClick="UpdateCategory" />
        <asp:Button ID="ButtonDeleteCategory" Text="Delete Category" Visible="false" runat="server" OnClick="DeleteCategory"/>
        <asp:Button ID="ButtonCancel" Text="Cancel" Visible="false" runat="server" OnClick="CancelChanges"/>
    </asp:Panel>

</asp:Content>
