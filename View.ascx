<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="View.ascx.vb" Inherits="CustomDNN.Modules.Thumbnail.View" %>
<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Label ID="panelName" runat="server"></asp:Label>
        </h3>
    </div>
    <div class="panel-body">
        <div class="grid">
            <asp:Repeater ID="thumbnailRepeat" runat="server" OnItemDataBound="thumbnailListOnItemDataBound" OnItemCommand="thumbnailListOnItemCommand">
                <ItemTemplate>
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="thumbnail">
                            <asp:Image ID="img" CssClass="img-responsive" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImgPath") %>' />
                            <div class="caption">
                                <h3><span><%# DataBinder.Eval(Container.DataItem, "Caption").ToString() %></span></h3>
                                <p>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description").ToString() %>' />
                                </p>
                                <p><a href='<%# DataBinder.Eval(Container.DataItem, "FilePath").ToString() %>' target="_blank" class="btn btn-primary" role="button">Download</a></p>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="adminPanel" Visible="false">
                            <asp:LinkButton ID="lnkEdit" runat="server" ResourceKey="EditThumbnail.Text" CommandName="Edit" Visible="false" Enabled="false" />
                            <asp:LinkButton ID="lnkDel" runat="server" ResourceKey="DeleteThumbnail.Text" CommandName="Delete" Visible="false" Enabled="false" />
                        </asp:Panel>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(window).load(function () {
        setTimeout(initMasonry, 1000);

    })
    function initMasonry() {
        $('.grid').masonry({
            itemSelector: '.col-md-3',
            columnWidth: '.col-md-3'
        })
    }
</script>
