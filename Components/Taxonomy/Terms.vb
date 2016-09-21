
Imports DotNetNuke.Entities.Content
Imports DotNetNuke.Entities.Content.Common

Namespace Components.Taxonomy

    Public Class Terms
        Public Sub ManageThumbnailTerms(objThumbnail As ThumbnailEntity, objContent As ContentItem)
            RemoveThumbnailTerms(objContent)

            For Each term In objThumbnail.Terms
                Util.GetTermController().AddTermToContent(term, objContent)
            Next

        End Sub

        Public Sub RemoveThumbnailTerms(objContent As ContentItem)
            Util.GetTermController().RemoveTermsFromContent(objContent)
        End Sub
    End Class
End Namespace
