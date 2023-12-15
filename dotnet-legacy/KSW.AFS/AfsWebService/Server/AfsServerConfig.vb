Imports System
Imports System.Collections.Generic
Imports System.IO.AbstractFilesystem

Public Class AfsServerConfig

  Private _Logons As New List(Of AfsLogonUserConfig)
  Public ReadOnly Property Logons As List(Of AfsLogonUserConfig)
    Get
      Return _Logons
    End Get
  End Property

  Private _Groups As New List(Of AfsGroupConfig)
  Public ReadOnly Property Groups As List(Of AfsGroupConfig)
    Get
      Return _Groups
    End Get
  End Property



End Class

Public Class AfsLogonUserConfig

  Public Property UserName As String

  Private _AttachedGroups As New List(Of String)
  Public ReadOnly Property AttachedGroups As List(Of String)
    Get
      Return _AttachedGroups
    End Get
  End Property

  Private _RootDir As New AfsVirtualDirectoryConfig
  Public ReadOnly Property RootDir As AfsVirtualDirectoryConfig
    Get
      Return _RootDir
    End Get
  End Property

End Class

Public Class AfsGroupConfig

  Public Property Name As String


  Private _RootDir As New AfsVirtualDirectoryConfig
  Public ReadOnly Property RootDir As AfsVirtualDirectoryConfig
    Get
      Return _RootDir
    End Get
  End Property

End Class