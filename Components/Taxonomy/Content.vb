Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Content
Imports DotNetNuke.Entities.Content.Common


Namespace Components.Taxonomy

    Public Class Content
        Private Const ContentTypeName As String = "Thumbnail"

        Public Function CreateContentItem(obj As ThumbnailEntity, tabID As Integer) As ContentItem
            Dim typeController = New ContentTypeController()
            Dim colContentTypes = typeController.GetContentTypes().Where(Function(o) o.ContentType = ContentTypeName)
            Dim contentTypeID As Integer

            If colContentTypes.Count > 0 Then
                Dim contentType = colContentTypes.Single()

                If contentType Is Nothing Then
                    contentTypeID = CreateContentType()
                Else
                    contentTypeID = contentType.ContentTypeId
                End If
            Else
                contentTypeID = CreateContentType()
            End If

            Dim objContent = New ContentItem()

            objContent.Content = obj.Name
            objContent.ContentTypeId = contentTypeID
            objContent.Indexed = False
            objContent.ContentKey = "tid=" & obj.ID
            objContent.ModuleID = obj.ModuleID
            objContent.TabID = tabID
            objContent.ContentItemId = Util.GetContentController().AddContentItem(objContent)

            Dim cntTerm As New Terms()
            cntTerm.ManageThumbnailTerms(obj, objContent)

            Return objContent
        End Function

        Public Sub UpdateContentItem(objThumbnail As ThumbnailEntity, tabID As Integer)
            Dim objContent = Util.GetContentController().GetContentItem(objThumbnail.ContentItemId)

            If objContent Is Nothing Then
                Return
            End If

            objContent.Content = objThumbnail.Name
            objContent.TabID = tabID
            Util.GetContentController().UpdateContentItem(objContent)

            Dim cntTerm As New Terms()
            cntTerm.ManageThumbnailTerms(objThumbnail, objContent)
        End Sub

        Public Sub DeleteContentItem(objThumbnail As ThumbnailEntity)
            If objThumbnail.ContentItemId <= Null.NullInteger Then
                Return
            End If

            Dim objContent = Util.GetContentController().GetContentItem(objThumbnail.ContentItemId)

            If objContent Is Nothing Then
                Return
            End If

            Dim cntTerm As New Terms()
            cntTerm.RemoveThumbnailTerms(objThumbnail)

            Util.GetContentController().DeleteContentItem(objContent)
        End Sub

#Region "Private methods"
        Private Shared Function CreateContentType() As Integer
            Dim typeController As New ContentTypeController()
            Dim objContentType As New ContentType()
            objContentType.ContentType = ContentTypeName

            Return typeController.AddContentType(objContentType)
        End Function
#End Region
    End Class

End Namespace
