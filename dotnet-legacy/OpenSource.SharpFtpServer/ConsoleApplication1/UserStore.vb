Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Xml.Serialization


Public Interface IUser
  Property UserName() As String
  Property HomeDir() As String
End Interface

Public Interface IUserManager
  Function Validate(username As String, password As String) As IUser
End Interface

Public Class UserManager
  Implements IUserManager

  Public Function Validate(username As String, password As String) As IUser Implements IUserManager.Validate
    Return UserStore.Validate(username, password)
  End Function

End Class


Public NotInheritable Class UserStore

  Private Sub New()
  End Sub

  Private Shared _users As List(Of User)

  Shared Sub New()
    _users = New List(Of User)()

    Dim serializer As New XmlSerializer(_users.[GetType](), New XmlRootAttribute("Users"))

    If File.Exists("users.xml") Then
      _users = TryCast(serializer.Deserialize(New StreamReader("users.xml")), List(Of User))
    Else
      _users.Add(New User() With { _
       .Username = "hns", _
       .Password = "test", _
       .HomeDir = "\" _
      })

      Using w As New StreamWriter("users.xml")
        serializer.Serialize(w, _users)
      End Using
    End If
  End Sub

  Public Shared Function Validate(username As String, password As String) As User
    Dim user As User = (From u In _users Where u.Username.ToLower = username.ToLower And u.Password = password Select u).SingleOrDefault()

    Return user
  End Function

End Class
