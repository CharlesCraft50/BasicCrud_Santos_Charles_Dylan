<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BasicCrud_Santos_Charles_Dylan._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .navbar-stone {
            background: url('https://static.vecteezy.com/system/resources/previews/003/678/912/original/stone-tiles-texture-in-cartoon-style-free-vector.jpg') repeat;
            background-size: cover;
            color: #E0E0C1;
            padding: 10px 0;
            border-bottom: 5px solid #D4AF37;
        }

        .navbar-stone a {
            color: #E0E0C1;
            font-family: 'Garamond', serif;
            font-size: 1.2em;
            padding: 10px 15px;
            text-decoration: none;
        }

        .navbar-stone a:hover {
            color: #D4AF37;
            text-decoration: none;
        }

        body {
            background: url('https://th.bing.com/th/id/OIP.QwoCNrqclWHVivUabMK5TgHaE8?rs=1&pid=ImgDetMain');
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .registration-form {
            background-color: #2E2E2E;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
            max-width: 800px;
            margin: auto;
        }

        .registration-form h1 {
            color: #D4AF37;
            text-align: center;
            margin-bottom: 20px;
        }

        .registration-form .form-group {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 15px;
        }

        .registration-form .form-group .form-column {
            flex-basis: calc(50% - 10px);
        }

        .registration-form label {
            color: #D4AF37;
            font-size: 1.2em;
            margin-bottom: 5px;
        }

        .registration-form input {
            background-color: #3E3E3E;
            color: #E0E0C1;
            border: none;
            padding: 10px;
            width: 100%;
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .registration-form button {
            background-color: #D4AF37;
            border: none;
            color: #2E2E2E;
            padding: 10px 20px;
            font-size: 1.2em;
            cursor: pointer;
            border-radius: 5px;
            width: 100%;
        }

        .registration-form button:hover {
            background-color: #B88900;
        }

        .gridview-container {
            margin: 20px auto;
            background-color: #2E2E2E;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
        }

        .gridview-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .gridview-container th, .gridview-container td {
            padding: 10px;
            border: 1px solid #D4AF37;
            text-align: left;
            color: #E0E0C1;
        }

        .gridview-container th {
            background-color: #3E3E3E;
        }

        .gridview-container td {
            background-color: #2E2E2E;
        }

        .gridview-container tr:nth-child(even) {
            background-color: #3E3E3E;
        }

        .gridview-container tr:hover {
            background-color: #D4AF37;
            color: #2E2E2E;
        }

        .navbar {
            display: none;
        }
    </style>

    <nav class="navbar-stone">
        <div class="container-fluid">
            <a href="/" class="navbar-brand">Home</a>
            <a href="/about">About</a>
            <a href="/services">Services</a>
            <a href="/contact">Contact</a>
        </div>
    </nav>

    <main class="container-fluid" style="background-color: #2E2E2E; color: #E0E0C1; font-family: 'Garamond', serif; padding-top: 20px;">
        <section class="row justify-content-center">
            <div class="col-md-12 registration-form">
                <h1>User Accounts CRUD</h1>
                <div class="gridview-container">
                    <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowEditing="GridViewUsers_RowEditing" OnRowUpdating="GridViewUsers_RowUpdating" OnRowCancelingEdit="GridViewUsers_RowCancelingEdit" OnRowDeleting="GridViewUsers_RowDeleting" OnRowCommand="GridViewUsers_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                            <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
                            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" />
                            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("UserID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:Panel ID="pnlAddNewUser" runat="server" Visible="False">
                    <h3>Add New User</h3>
                    <label>Last Name:</label>
                    <asp:TextBox ID="txtNewLastName" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>First Name:</label>
                    <asp:TextBox ID="txtNewFirstName" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Age:</label>
                    <asp:TextBox ID="txtNewAge" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Birthday:</label>
                    <asp:TextBox ID="txtNewBirthday" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Contact Number:</label>
                    <asp:TextBox ID="txtNewContactNumber" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Address:</label>
                    <asp:TextBox ID="txtNewAddress" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <asp:Button ID="btnSaveNewUser" runat="server" Text="Save" CssClass="btn btn-default" />
                </asp:Panel>

                <asp:Panel ID="pnlEditUser" runat="server" Visible="False">
                    <h3>Edit User</h3>
                    <label>Last Name:</label>
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>First Name:</label>
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Age:</label>
                    <asp:TextBox ID="txtEditAge" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Birthday:</label>
                    <asp:TextBox ID="txtEditBirthday" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Contact Number:</label>
                    <asp:TextBox ID="txtEditContactNumber" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <label>Address:</label>
                    <asp:TextBox ID="txtEditAddress" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <asp:Button ID="btnUpdateUser" runat="server" Text="Update" OnClick="btnUpdateUser_Click" CssClass="btn btn-default" />
                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" CssClass="btn btn-default" />
                    <asp:HiddenField ID="hfEditUserID" runat="server" />
                </asp:Panel>

                <asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" CssClass="btn btn-default" />
            </div>
        </section>
    </main>
</asp:Content>

