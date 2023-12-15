Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

<Serializable()> _
Public Class User
  Implements IUser

  <XmlAttribute("username")> _
  Public Property Username() As String Implements IUser.UserName
    Get
      Return m_Username
    End Get
    Set(value As String)
      m_Username = value
    End Set
  End Property
  Private m_Username As String

  <XmlAttribute("password")> _
  Public Property Password() As String
    Get
      Return m_Password
    End Get
    Set(value As String)
      m_Password = value
    End Set
  End Property
  Private m_Password As String

  <XmlAttribute("homedir")> _
  Public Property HomeDir() As String Implements IUser.HomeDir
    Get
      Return m_HomeDir
    End Get
    Set(value As String)
      m_HomeDir = value
    End Set
  End Property
  Private m_HomeDir As String
End Class

