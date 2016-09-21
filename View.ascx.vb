' Copyright (c) 2016  Veloxcore.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
Imports DotNetNuke
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports CustomDNN.Modules.Thumbnail.Components
Imports DotNetNuke.UI.Utilities
Imports System.IO

''' <summary>
''' The View class displays the content
''' 
''' Typically your view control would be used to display content or functionality in your module.
''' 
''' View may be the only control you have in your project depending on the complexity of your module
''' 
''' Because the control inherits from ThumbnailModuleBase you have access to any custom properties
''' defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
''' 
''' </summary>
Public Class View
    Inherits ThumbnailModuleBase
    Implements IActionable

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Page_Load runs when the control is loaded
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Settings.Contains("PanelName") Then
                panelName.Text = Settings("PanelName").ToString()
            End If
            Dim portalHomePath As String = System.IO.Path.Combine(PortalSettings.HomeDirectory, "Thumbnail")
            Dim data = ThumbnailController.GetThumbnails(ModuleId)
            data.ForEach(Function(o)
                             o.ImgPath = System.IO.Path.Combine(portalHomePath, "images", o.ImgPath)
                             o.FilePath = System.IO.Path.Combine(portalHomePath, "files", o.FilePath)
                             Return o
                         End Function)
            thumbnailRepeat.DataSource = data
            thumbnailRepeat.DataBind()
        Catch exc As Exception
            Exceptions.ProcessModuleLoadException(Me, exc)
        End Try

    End Sub

    Protected Sub thumbnailListOnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lnkEdit As LinkButton = e.Item.FindControl("lnkEdit")
            Dim lnkDel As LinkButton = e.Item.FindControl("lnkDel")

            Dim pnlAdminControls As Panel = e.Item.FindControl("adminPanel")

            Dim currentThumbnail As ThumbnailEntity = e.Item.DataItem

            If IsEditable And lnkDel IsNot Nothing And lnkEdit IsNot Nothing And pnlAdminControls IsNot Nothing Then
                pnlAdminControls.Visible = True
                lnkEdit.Attributes.Add("href", EditUrl(String.Empty, String.Empty, "Edit", "tid=" & currentThumbnail.ID.ToString()))

                lnkDel.CommandArgument = currentThumbnail.ID.ToString()
                lnkEdit.Enabled = True
                lnkEdit.Visible = True
                lnkDel.Visible = True
                lnkDel.Enabled = True

                ClientAPI.AddButtonConfirm(lnkDel, Localization.GetString("ConfirmDelete", LocalResourceFile))

            Else
                pnlAdminControls.Visible = False
            End If
        End If
    End Sub

    Public Sub thumbnailListOnItemCommand(source As Object, e As RepeaterCommandEventArgs)

        If e.CommandName = "Delete" Then
            Dim thumbnail As ThumbnailEntity = ThumbnailController.GetThumbnail(Convert.ToInt32(e.CommandArgument))
            ThumbnailController.DeleteThumbnail(Convert.ToInt32(e.CommandArgument))

            If File.Exists(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/images", thumbnail.ImgPath)) Then
                File.Delete(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/images", thumbnail.ImgPath))
            End If

            If File.Exists(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/files", thumbnail.FilePath)) Then
                File.Delete(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/files", thumbnail.FilePath))
            End If

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL())
        End If

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Registers the module actions required for interfacing with the portal framework
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property ModuleActions() As ModuleActionCollection Implements IActionable.ModuleActions
        Get
            Dim Actions As New ModuleActionCollection
            Actions.Add(GetNextActionID, Localization.GetString("EditModule", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
            Return Actions
        End Get
    End Property

End Class