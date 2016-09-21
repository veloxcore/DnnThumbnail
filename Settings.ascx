<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="Settings.ascx.vb" Inherits="CustomDNN.Modules.Thumbnail.Settings" %>



<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
<fieldset>
    <div class="dnnFormItem">
        <dnn:label id="lblName" runat="server" text="Name" HelpText="Enter the name of the panel"></dnn:label>
        <asp:TextBox ID="txtName" runat="server" />
    </div>
</fieldset>
