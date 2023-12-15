Imports System.Runtime.CompilerServices
Imports System.ComponentModel
Imports System.IO

Public Module Extensions

  <Extension(), EditorBrowsable(EditorBrowsableState.Always)>
  Public Function GetSubDirectory(currentDirectory As DirectoryInfo, relativePath As String) As DirectoryInfo
    Dim relativePathParts As String() = relativePath.Split(Path.DirectorySeparatorChar)
    For Each part In relativePathParts
      Select Case part
        Case ".." : currentDirectory = currentDirectory.Parent
        Case "."
        Case ""
        Case Else
          currentDirectory = New DirectoryInfo(Path.Combine(currentDirectory.FullName, part))
      End Select
    Next
    Return currentDirectory
  End Function


End Module
