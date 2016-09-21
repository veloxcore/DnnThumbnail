<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="Edit.ascx.vb" Inherits="CustomDNN.Modules.Thumbnail.Edit" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>


<form role="form">
    <asp:Label ID="lblErrors" Visible="false" ForeColor="Red" runat="server"></asp:Label>
    <div class="form-group">
        <dnn:label id="lblName" runat="server" text="Name" HelpText="Enter the name of thumbnail or by default it will take Thumbnail as Name"></dnn:label>
        <asp:TextBox ID="txtName" runat="server" Text="Thumbnail"></asp:TextBox>
    </div>
    <div class="form-group">
        <dnn:label id="lblImg" runat="server" text="Image" HelpText="Give image path"></dnn:label>
        <asp:FileUpload ID="fuImage" runat="server" AllowMultiple="false" accept=".png,.jpg,.jpeg,.gif" />
    </div>
    <div class="form-group">
        <dnn:label id="lblCaption" runat="server" text="Caption" HelpText="Give Caption"></dnn:label>
        <asp:TextBox ID="txtCaption" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <dnn:label id="lblDesc" runat="server" text="Description" HelpText="Give description" ></dnn:label>
        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="5" Columns="20"></asp:TextBox>
    </div>
    <div class="form-group">
        <dnn:label id="lblFile" runat="server" text="FilePath" HelpText="Give file path, if you will not give any file to download it will take as null"></dnn:label>
        <asp:FileUpload ID="fuFile" runat="server" AllowMultiple="false" />
    </div>
    <asp:LinkButton ID="btnSubmit" Text="Save Thumbnail" runat="server" CssClass="btn btn-primary" />
    &nbsp;
    <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CssClass="link" />
</form>
