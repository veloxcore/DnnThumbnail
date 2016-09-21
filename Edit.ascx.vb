' Copyright (c) 2016  Veloxcore.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports CustomDNN.Modules.Thumbnail.Components
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
Public Class Edit
    Inherits ThumbnailModuleBase


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
            If Not Page.IsPostBack Then
                If ThumbnaiID > 0 Then
                    Dim thumbnail As ThumbnailEntity = ThumbnailController.GetThumbnail(ThumbnaiID)

                    If thumbnail IsNot Nothing Then
                        txtName.Text = thumbnail.Name
                        txtCaption.Text = thumbnail.Caption
                        txtDesc.Text = thumbnail.Description

                    End If
                End If
            End If
        Catch exc As Exception
            Exceptions.ProcessModuleLoadException(Me, exc)
        End Try

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim thumbnail As New ThumbnailEntity
        Dim imageName As String
        Dim fileName As String
        Dim uniqueID As String = Guid.NewGuid().ToString()
        Dim imageTypes As New List(Of String)

        If ThumbnaiID > 0 Then
            thumbnail = ThumbnailController.GetThumbnail(ThumbnaiID)

            thumbnail.Name = txtName.Text.Trim().ToString()
            thumbnail.Caption = txtCaption.Text.Trim().ToString()
            thumbnail.Description = txtDesc.Text.ToString()
            thumbnail.LastModifiedByUserID = UserId
            thumbnail.LastModifiedOnDate = DateTime.Now
        Else

            If txtName.Text.Trim() = "" Then
                thumbnail.Name = "Thumbnail"
            Else
                thumbnail.Name = txtName.Text.Trim().ToString()
            End If

            thumbnail.Caption = txtCaption.Text.Trim().ToString()
            thumbnail.Description = txtDesc.Text.ToString()
            thumbnail.CreatedByUserID = UserId
            thumbnail.CreatedOnDate = DateTime.Now
            thumbnail.ModuleID = ModuleId
        End If

        If fuImage.HasFile And fuImage.PostedFile.ContentType.Contains("image") Then
            Dim imgDirectoryPath As String = Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/images")
            If Not System.IO.Directory.Exists(imgDirectoryPath) Then
                System.IO.Directory.CreateDirectory(imgDirectoryPath)
            End If

            imageName = uniqueID & Path.GetExtension(fuImage.PostedFile.FileName)
            fuImage.SaveAs(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/images", imageName))
            thumbnail.ImgPath = imageName
        ElseIf ThumbnaiID <= 0 Then
            lblErrors.Text = "Image field is not image type, please select image in image field below or upload image."
            lblErrors.Visible = True
            Return
        End If

        If fuFile.HasFile Then
            Dim fileDirectoryPath As String = Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/files")
            If Not System.IO.Directory.Exists(fileDirectoryPath) Then
                System.IO.Directory.CreateDirectory(fileDirectoryPath)
            End If

            fileName = Path.GetFileNameWithoutExtension(fuFile.PostedFile.FileName) & "_" & uniqueID & Path.GetExtension(fuFile.PostedFile.FileName)
            fuFile.SaveAs(Path.Combine(PortalSettings.HomeDirectoryMapPath, "Thumbnail/files", fileName))
            thumbnail.FilePath = fileName
        ElseIf ThumbnaiID <= 0 Then
            lblErrors.Text = "Please upload file in the file field."
            lblErrors.Visible = True
            Return
        End If
        ThumbnailController.SaveThumbnail(thumbnail, TabId)
        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL())

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL())
    End Sub
End Class