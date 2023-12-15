Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Net.Sockets
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Public Class DataConnectionOperation

  Private _Operation As Func(Of NetworkStream, String, IFileSystemProvider, String)
  Private _Arguments As String

  Public Property Operation() As Func(Of NetworkStream, String, IFileSystemProvider, String)
    Get
      Return _Operation
    End Get
    Set(value As Func(Of NetworkStream, String, IFileSystemProvider, String))
      _Operation = value
    End Set
  End Property

  Public Property Arguments() As String
    Get
      Return _Arguments
    End Get
    Set(value As String)
      _Arguments = value
    End Set
  End Property

End Class
