

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Content

Namespace Components

    Public Class ThumbnailEntity
        Inherits ContentItem

        Public Property ID As Integer

        Public Property Name As String

        Public Property ImgPath As String

        Public Property Caption As String

        Public Property Description As String

        Public Property FilePath As String

        Public Overloads Property CreatedByUserID As Integer

        Public Overloads Property CreatedOnDate As Date

        Public Overloads Property LastModifiedByUserID As Integer

        Public Overloads Property LastModifiedOnDate As Date

        Public Overloads Property ModuleID As Integer

#Region "IHydratable implementation"
        Public Overrides Sub Fill(dr As IDataReader)
            'MyBase.Fill(dr)

            ID = Null.SetNullInteger(dr("ID"))
            Name = Null.SetNullString(dr("Name"))
            ImgPath = Null.SetNullString(dr("ImgPath"))
            Caption = Null.SetNullString(dr("Caption"))
            Description = Null.SetNullString(dr("Description"))
            FilePath = Null.SetNullString(dr("FilePath"))
            CreatedByUserID = Null.SetNullInteger(dr("CreatedByUserID"))
            CreatedOnDate = Null.SetNullDateTime(dr("CreatedOnDate"))
            LastModifiedByUserID = Null.SetNullInteger(dr("LastModifiedByUserID"))
            LastModifiedOnDate = Null.SetNullDateTime(dr("LastModifiedOnDate"))
            ModuleID = Null.SetNullInteger(dr("ModuleID"))
            ContentItemId = Null.SetNullInteger(dr("ContentItemId"))
        End Sub

        Public Overrides Property KeyID As Integer
            Get
                Return ID
            End Get
            Set(value As Integer)
                ID = value
            End Set
        End Property
#End Region
    End Class
End Namespace
