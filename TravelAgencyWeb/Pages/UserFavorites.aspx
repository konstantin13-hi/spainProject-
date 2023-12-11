<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserFavorites.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserFavorites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Favorites</h1>
        
               
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>

    

                
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
        <asp:TextBox ID="txtCity" runat="server" AutoPostBack="true" OnTextChanged="txtCity_TextChanged"></asp:TextBox>

        <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label>
        <asp:TextBox ID="txtCountry" runat="server" AutoPostBack="true" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>

           
    
   
                

                Filter by
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
               
                    <asp:ListItem Value ="hasswimmingpool">SwimmingPool</asp:ListItem>
                    <asp:ListItem Value="hasgym">Gym</asp:ListItem>
                    <asp:ListItem Value="ispetfriendly">PetFriendly</asp:ListItem>
                    <asp:ListItem Value="hasparking">Parking</asp:ListItem>
                 
                </asp:CheckBoxList>
                

            
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Unnamed1_SelectedIndexChanged">
                    <asp:ListItem>price increasing</asp:ListItem>
                    <asp:ListItem>price decreasing</asp:ListItem>
                    <asp:ListItem>star ranking</asp:ListItem>
                </asp:DropDownList>
        <br>
           guest score
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                  <asp:ListItem Value="0">any</asp:ListItem>
                    <asp:ListItem Value="9">excelent (9 or more) </asp:ListItem>
                    <asp:ListItem Value="7">good (7 or more)</asp:ListItem>
                    <asp:ListItem Value="8">very good(8 or more)</asp:ListItem>
</asp:RadioButtonList>

        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
    <asp:ListItem Value="">All Categories</asp:ListItem>
</asp:DropDownList>

        <br>
        price
        <br>
  <asp:TextBox ID="TextBoxMinPrice" runat="server" placeholder="Min Price" OnTextChanged="TextBoxMinPrice_TextChanged"></asp:TextBox>
<asp:TextBox ID="TextBoxMaxPrice" runat="server" placeholder="Max Price" OnTextChanged="TextBoxMaxPrice_TextChanged"></asp:TextBox>
<asp:Button ID="ButtonFilter" runat="server" Text="Filter" OnClick="ButtonFilter_Click" />
     
            <asp:GridView ID="hotelsGridView" AutoGenerateColumns="False"
                EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
                AutoGenerateSelectButton="True" runat="server" OnSelectedIndexChanged="hotelsGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="name" HeaderText="Name" />
                            <asp:BoundField DataField="average_rating" HeaderText="Average Rating" />
                             <asp:BoundField DataField="price" HeaderText="Price" />
                            <asp:BoundField DataField="hasswimmingpool" HeaderText="pool" />
                            <asp:BoundField DataField="ispetfriendly" HeaderText="Pet" />
                            <asp:BoundField DataField="hasparking" HeaderText="Parking" />
                            <asp:BoundField DataField="hasgym" HeaderText="Gym" />
                               <asp:BoundField DataField="city" HeaderText="town" />
                               <asp:BoundField DataField="country" HeaderText="country" />
                              <asp:BoundField DataField="category" HeaderText="category" />

                            <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="delete-button" CommandName="Delete" CommandArgument='<%# Eval("id") %>' OnClick="btnDelete_Click" />
            </ItemTemplate>
        </asp:TemplateField>
                        </Columns>
                  

            </asp:GridView>
    
          </script>
              </ContentTemplate>
           <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
             <asp:AsyncPostBackTrigger ControlID="ButtonFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="CheckBoxList1" EventName="SelectedIndexChanged" />


       
    </Triggers>
</asp:UpdatePanel>
  
    
</asp:Content>
