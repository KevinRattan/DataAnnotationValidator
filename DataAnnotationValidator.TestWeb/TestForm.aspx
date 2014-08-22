<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="DataAnnotationValidatorTestWeb.TestForm" %>

<%@ Register Assembly="DataAnnotationValidator" Namespace="Validators" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <asp:FormView ID="TestFormView" runat="server" SelectMethod="TestFormView_GetItem" InsertMethod="TestFormView_InsertItem"
                ItemType="DataAnnotationValidatorTestWeb.Models.TestModel" DefaultMode="Insert">
                <InsertItemTemplate>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="NameTextBox" Text="Name: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.Name %>' runat="server" ID="NameTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator1" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="Name" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="NameTextBox" Text="*"></cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="EmailTextBox" Text="Email: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.Email %>' runat="server" ID="EmailTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator2" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="Email" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="EmailTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="TelephoneTextBox" Text="Telephone: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.Telephone %>' runat="server" ID="TelephoneTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator3" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="Telephone" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="TelephoneTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="UrlTextBox" Text="Url: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.Url %>' runat="server" ID="UrlTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator4" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="Url" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="UrlTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="AgeTextBox" Text="Age: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.Age %>' runat="server" ID="AgeTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator5" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="Age" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="AgeTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="ZipCodeTextBox" Text="ZipCode: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.ZipCode %>' runat="server" ID="ZipCodeTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator7" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="ZipCode" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="ZipCodeTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="CreditCardTextBox" Text="CreditCard: "></asp:Label><br />
                    <asp:TextBox Text='<%# BindItem.CreditCard %>' runat="server" ID="CreditCardTextBox" />
                    <cc1:DataAnnotationValidator ID="DataAnnotationValidator6" runat="server"
                        TypeName="DataAnnotationValidatorTestWeb.Models.TestModel" TypeProperty="CreditCard" TypeAssembly="DataAnnotationValidatorTestWeb"
                        Display="Dynamic" ControlToValidate="CreditCardTextBox" Text="*">  </cc1:DataAnnotationValidator><br />
                    <asp:Button runat="server" Text="Insert" CommandName="Insert" ID="InsertButton" CausesValidation="True" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Error messages from data annotations" />
                </InsertItemTemplate>
            </asp:FormView>
        </div>



    </form>
</body>
</html>
