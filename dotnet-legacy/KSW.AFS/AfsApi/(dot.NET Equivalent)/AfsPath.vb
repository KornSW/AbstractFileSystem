Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text

Public Class AfsPath

  Private Sub New()
  End Sub


  ''' <summary>
  ''' Converts '\a\..\..\b\c\d\..\' into '\..\b\c\"
  ''' </summary>
  Public Shared Function Defragment(fragmentedPath As String, Optional prependSeparator As Boolean = False, Optional appendSeparator As Boolean = False) As String
    Dim parts As String() = fragmentedPath.Split(Path.DirectorySeparatorChar)
    Dim result As New List(Of String)
    Dim startIndex As Integer = 0
    Dim endIndex As Integer = parts.Length - 1

    If (fragmentedPath.StartsWith(Path.DirectorySeparatorChar)) Then
      startIndex = 1
      prependSeparator = True
    End If
    If (fragmentedPath.EndsWith(Path.DirectorySeparatorChar)) Then
      endIndex -= 1
      appendSeparator = True
    End If

    For i As Integer = startIndex To endIndex
      If (parts(i) = "..") Then
        If (result.Count > 0) Then
          result.RemoveAt(result.Count - 1)
        Else
          result.Add("..")
        End If
      Else
        result.Add(parts(i))
      End If
    Next

    If (prependSeparator) Then
      result.Insert(0, String.Empty)
    End If
    If (appendSeparator) Then
      result.Add(String.Empty)
    End If

    Return String.Join(Path.DirectorySeparatorChar, result.ToArray())
  End Function

  Public Shared Function GetName(afsPath As String) As String
    Dim parts As String() = Defragment(afsPath).Split(Path.DirectorySeparatorChar)
    If (parts.Length > 1 AndAlso parts(parts.Length - 1) = String.Empty) Then
      Return parts(parts.Length - 2)
    Else
      Return parts(parts.Length - 1)
    End If
  End Function

End Class
