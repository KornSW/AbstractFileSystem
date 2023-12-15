Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO.AbstractFilesystem
Imports System.Linq
Imports System.Runtime.CompilerServices

Public Class FileList

  Private _Directory As IAfsDirectoryInfo = Nothing
  Public Property Directory As IAfsDirectoryInfo
    Get
      Return _Directory
    End Get
    Set(value As IAfsDirectoryInfo)
      _Directory = value
      Me.Reload()
    End Set
  End Property

  Public Property View As View
    Get
      Return ListView1.View
    End Get
    Set(value As View)
      ListView1.View = value
    End Set
  End Property

  Public Sub Reload()
    Me.ListView1.Items.Clear()
    If (_Directory Is Nothing) Then
      Exit Sub
    End If

    For Each file In _Directory.GetFiles()
      Dim newItem As New ListViewItem(file.Name)
      Dim ext = IO.Path.GetExtension(file.Name).ToLower()
      Me.EnsureFileImageExists(ext)
      newItem.ImageKey = ext
      newItem.SubItems.Add(Me.GetSizeString(file.Length))
      newItem.SubItems.Add(file.LastWriteTime.ToShortDateString())
      newItem.SubItems.Add(file.CreationTime.ToShortDateString())
      Me.ListView1.Items.Add(newItem)
    Next

  End Sub

  Private Sub EnsureFileImageExists(extension As String)
    If (Not ILSmall.Images.ContainsKey(extension)) Then
      ILSmall.Images.Add(extension, KSW.Base.FileIconManager.FindIconForFilename("dummy" & extension, False).ToBitmap())
      ILBig.Images.Add(extension, KSW.Base.FileIconManager.FindIconForFilename("dummy" & extension, True).ToBitmap())
    End If
  End Sub

  Private Function GetSizeString(bytes As Double) As String
    Dim names As String() = {"Byte", "KB", "MB", "GB", "TB", "PB", "EB"}
    Dim level As Integer = 0
    While bytes >= 1024 AndAlso Not level >= names.Length
      level += 1
      bytes = bytes / 1024
    End While
    Return String.Format("{0} {1}", Math.Round(bytes, 1), names(level))
  End Function

End Class
