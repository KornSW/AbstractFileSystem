Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO.AbstractFilesystem
Imports System.Linq
Imports System.Runtime.CompilerServices

Public Class FileContract

  Public Sub New()
  End Sub

  Public Sub New(file As AfsFileInfo)
    Me.Name = file.Name
    Me.LastWriteTime = file.LastWriteTime
    Me.CreationTime = file.CreationTime
    Me.Length = file.Length
  End Sub

  Public Property Name As String
  Public Property LastWriteTime As DateTime
  Public Property CreationTime As DateTime
  Public Property Length As Long

End Class

Public Class DirectoryContract

  Public Sub New()
  End Sub

  Public Sub New(dir As AfsDirectoryInfo)
    Me.Name = dir.Name
    Me.HasFiles = dir.GetFiles.Any()
    Me.HasSubdirs = dir.GetDirectories().Any()
    Me.IsAccessable = dir.IsAccessable
  End Sub

  Public Property Name As String = String.Empty
  Public Property HasSubdirs As Boolean = False
  Public Property HasFiles As Boolean = False
  Public Property IsAccessable As Boolean = True


End Class

Public Class StreamStateInfo

  Public Property CanRead As Boolean
  Public Property CanSeek As Boolean
  Public Property CanWrite As Boolean
  Public Property Position As Long
  Public Property Length As Long

End Class

Public Class StreamReadContract
  Inherits StreamStateInfo

  Public Property Result As Integer
  Public Property Data As Byte()

End Class
