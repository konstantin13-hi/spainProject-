<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="Accomodation.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <h1>Accomodation</h1>



      






                 <asp:Button runat="server" Text="View all accommodation options" class="search_head_button" OnClick=ReturnToBrowser></asp:Button>
                <asp:Label ID="Label_accId" runat="server" Text=""></asp:Label>
           

               <asp:GridView ID="hotelsGridView" runat="server" AutoGenerateColumns="True">
                    <Columns>
                    <asp:BoundField DataField="name" HeaderText="Name" />
                          </Columns>
</asp:GridView>
               


                <h4>Points of Accommodation</h4>
                <asp:Button ID="Button1_giveReview" runat="server" Text="Give Review" OnClick="Button1_giveReview_Click" />
                <p>
                <asp:Label ID="Label1_avg" runat="server" Text="Looks like there is no grades yet"></asp:Label>
                <br />
                <asp:Label ID="Label2_overall" runat="server" Text="Looks like there is no grades yet"></asp:Label>
                <br />
                <asp:Label ID="Label3_location" runat="server" Text="Looks like there is no grades yet"></asp:Label>
                <br />
                <asp:Label ID="Label4_cleanliness" runat="server" Text="Looks like there is no grades yet"></asp:Label>
                <br />
                
                </p>
       
              
  


                <div><h4>Reviews of other customers</h4>
                <asp:DataList ID="DataList1_reviews" runat="server" RepeatColumns="1" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <asp:Label ID="reviewTextLabel" runat="server" Text='<%# Eval("reviewText") %>' />
                        <br />
<br />
                    </ItemTemplate>
                    </asp:DataList>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbconnection %>" SelectCommand="SELECT [reviewText] FROM [reviews] WHERE ([accomodation_id] = @accomodation_id)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label_accId" Name="accomodation_id" PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
    


         

        
   
             <asp:Button runat="server" Text="Book" class="data_button" OnClick="Unnamed4_Click"></asp:Button>


    <asp:Button ID="btnAddToFavorites" runat="server" Text="Add to favorites" OnClick="btnAddToFavorites_Click" />

                                                   <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Warning</h4>
                                    </div>
                                    <div class="modal-body">
                                        <p>You need to register to add to favorites.</p>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnGoToRegistration" runat="server" Text="Go to Registration" OnClick="btnGoToRegistration_Click" CssClass="btn btn-primary" />
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Stay on Page</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                                        <div id="myModalSecond" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Warning</h4>
                                </div>
                                <div class="modal-body">
                                    <p>You need to register to book.</p>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button1" runat="server" Text="Go to Registration" OnClick="btnGoToRegistration_Click" CssClass="btn btn-primary" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Stay on Page</button>
                                </div>
                            </div>
                        </div>
                    </div>


 
</asp:Content>

