
Imports CustomDNN.Modules.Thumbnail.Data
Imports DotNetNuke.Common.Utilities

Namespace Components
    Public Class ThumbnailController
        Public Shared Function GetThumbnail(id As Integer) As ThumbnailEntity
            Return CBO.FillObject(Of ThumbnailEntity)(DataProvider.Instance().GetThumbnail(id))
        End Function

        Public Shared Function GetThumbnails(moduleID As Integer) As List(Of ThumbnailEntity)
            Return CBO.FillCollection(Of ThumbnailEntity)(DataProvider.Instance().GetThumbnails(moduleID))
        End Function

        Public Shared Sub DeleteThumbnail(id As Integer)
            DataProvider.Instance().DeleteThumbnail(id)
        End Sub

        Public Shared Sub DeleteThumbnails(moduleId As Integer)
            DataProvider.Instance().DeleteThumbnails(moduleId)
        End Sub

        Public Shared Function SaveThumbnail(obj As ThumbnailEntity, tabID As Integer) As Integer
            If obj.ID < 1 Then
                obj.LastModifiedByUserID = obj.CreatedByUserID
                obj.LastModifiedOnDate = obj.CreatedOnDate

                Dim cntTaxonomy As New Taxonomy.Content()
                Dim objContentItem = cntTaxonomy.CreateContentItem(obj, tabID)
                obj.ContentItemId = objContentItem.ContentItemId
                obj.ID = DataProvider.Instance().AddThumbnail(obj)

            Else
                DataProvider.Instance().UpdateThumbnail(obj)
                Dim cntTaxonomy As New Taxonomy.Content()
                cntTaxonomy.UpdateContentItem(obj, tabID)
            End If

            Return obj.ID
        End Function
    End Class

End Namespace
