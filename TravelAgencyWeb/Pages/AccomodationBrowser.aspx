<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AccomodationBrowser.aspx.cs" Inherits="TravelAgencyWeb.Pages.AccomodationBrowser" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="css/accomodationBrow.css">
       <link rel="stylesheet" href="css/bootstrap-reboot.min.css">
       <link rel="stylesheet" href="css/bootstap-grid.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/ajaxcontroltoolkit/17.1.1/AjaxControlToolkit.js"></script>

      <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/smoothness/jquery-ui.css">


    <h1>Accomodation Browser</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

               
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>

     

<script type="text/javascript">
    $(document).ready(function () {
        // Функция для настройки автозаполнения
        function setupAutocomplete(selector, url) {
            $(document).on("keydown.autocomplete", selector, function () {
                $(this).autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: JSON.stringify({ prefix: request.term }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                console.log(result);
                            }
                        });
                    },
                    minLength: 1
                });
            });
        }

        // Настройка автозаполнения для поля txtCountry
        setupAutocomplete("#<%=txtCountry.ClientID%>", "AccomodationBrowser.aspx/GetCountries");

        // Настройка автозаполнения для поля txtCity
        setupAutocomplete("#<%=txtCity.ClientID%>", "AccomodationBrowser.aspx/GetCities");
    });
</script>

       <div class="research">
    <div class="container">
        <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
           <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>

      

        <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label>
        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
     
    </div>
</div>

           
<div class="container">
    <div class="row">
        <div class="col-md-3 offset-md-1">
             <div class="checkboxlist-wrapper" >
    <asp:Label ID="Label1" runat="server" CssClass="checkboxlist-label" Text="Options:"></asp:Label>
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
        <asp:ListItem Value="hasswimmingpool">SwimmingPool</asp:ListItem>
        <asp:ListItem Value="hasgym">Gym</asp:ListItem>
        <asp:ListItem Value="ispetfriendly">PetFriendly</asp:ListItem>
        <asp:ListItem Value="hasparking">Parking</asp:ListItem>
    </asp:CheckBoxList>
</div>

<div class="dropdownlist-wrapper">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Unnamed1_SelectedIndexChanged">
        <asp:ListItem>price increasing</asp:ListItem>
        <asp:ListItem>price decreasing</asp:ListItem>
        <asp:ListItem>star ranking</asp:ListItem>
    </asp:DropDownList>
</div>


            <div class="radiobuttonlist-wrapper">
    <asp:Label ID="Label2" runat="server" CssClass="radiobuttonlist-label" Text="Guest score:"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
        <asp:ListItem Value="0">any</asp:ListItem>
        <asp:ListItem Value="9">excellent (9 or more)</asp:ListItem>
        <asp:ListItem Value="7">good (7 or more)</asp:ListItem>
        <asp:ListItem Value="8">very good (8 or more)</asp:ListItem>
    </asp:RadioButtonList>
</div>
         <div class="textbox-wrapper">
    <asp:Label ID="lblPrice" runat="server" CssClass="textbox-label" Text="Price range:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBoxMinPrice" runat="server" placeholder="Min" OnTextChanged="TextBoxMinPrice_TextChanged"></asp:TextBox>
    <asp:TextBox ID="TextBoxMaxPrice" runat="server" placeholder="Max" OnTextChanged="TextBoxMaxPrice_TextChanged"></asp:TextBox>
        
    <asp:Button ID="ButtonFilter" runat="server" CssClass="filter-button"  Text="Filter" OnClick="ButtonFilter_Click" />
 



     
</div>

        


           

  
            

        </div>
       
        <div class="col-md-9">
            <div class="dropdownlist-wrapper">
   <div class="dropdownlist-wrapper">
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
        <asp:ListItem Value="">All Categories</asp:ListItem>
    </asp:DropDownList>
</div>
           
            <div> 

      <asp:GridView ID="hotelsGridView" AutoGenerateColumns="False" EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
    AutoGenerateSelectButton="True" runat="server" OnSelectedIndexChanged="hotelsGridView_SelectedIndexChanged"
    CssClass="hotel-card-grid">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-HorizontalAlign="Right" />
        <asp:TemplateField HeaderText="Hotel">
            <ItemTemplate>
                <div class="hotel-card" onclick="redirectToHotelPage('<%# Eval("id") %>')" onmouseover="highlightCard(this)" onmouseout="unhighlightCard(this)">
                    <h4><%# Eval("name") %></h4>
                    <p>Rating: <%# Eval("average_rating", "{0:f1}") %></p>
                    <p>Price: $<%# Eval("price") %></p>
                    <p>Pool: <%# Eval("hasswimmingpool") %></p>
                    <p>Pet-friendly: <%# Eval("ispetfriendly") %></p>
                    <p>Parking: <%# Eval("hasparking") %></p>
                    <p>Gym: <%# Eval("hasgym") %></p>
                    <p>Town: <%# Eval("city") %></p>
                    <p>Country: <%# Eval("country") %></p>
                    <p>Category: <%# Eval("category") %></p>
                 
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<script>
    function highlightCard(card) {
        card.style.boxShadow = "0 0 10px rgba(0, 0, 0, 0.3)";
    }

    function unhighlightCard(card) {
        card.style.boxShadow = "";
    }

    function redirectToHotelPage(hotelId) {
        <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-HorizontalAlign="Right" />
        // Перенаправление на страницу отеля с использованием hotelId
       // window.location.href = "hotel.aspx?id=" + hotelId;
    }
</script>

                </div>
        </div>
    </div>
</div>
    
                
      
    
           
     
              </ContentTemplate>
     
           <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
             <asp:AsyncPostBackTrigger ControlID="ButtonFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="CheckBoxList1" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtCity" EventName="TextChanged" />
                 <asp:AsyncPostBackTrigger ControlID="txtCountry" EventName="TextChanged" />
                 </Triggers>
     
      </asp:UpdatePanel> 
  
    </asp:Content>
