Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices

Public Class AfsVirtualDirectoryConfig

  Private _SubDirs As New Dictionary(Of String, AfsVirtualDirectoryConfig)
  Public ReadOnly Property SubDirs As Dictionary(Of String, AfsVirtualDirectoryConfig)
    Get
      Return _SubDirs
    End Get
  End Property

  Private _MountedDirs As New Dictionary(Of String, String)
  Public ReadOnly Property MountedDirs As Dictionary(Of String, String)
    Get
      Return _MountedDirs
    End Get
  End Property




End Class
