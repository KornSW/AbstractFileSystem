
Public Enum FileSystemAccessMessages As Integer

  DirectoryNotFound = 550
  ChangedToNewDirectory = 250
  FileNotFound = 5500
  ddd
End Enum

Public Class FsMessages

  Public Shared Function GetMessage(fsMessage As FileSystemAccessMessages) As String

    Select Case fsMessage
      Case FileSystemAccessMessages.DirectoryNotFound : Return "550 Directory Not Found"
      Case FileSystemAccessMessages.ChangedToNewDirectory : Return "250 Changed to new directory"
      Case FileSystemAccessMessages.FileNotFound : Return "550 File Not Found"
      Case FileSystemAccessMessages.ddd : Return "250 Requested file action okay, completed"
      Case FileSystemAccessMessages.ddd : Return "550 Directory already exists"
      Case FileSystemAccessMessages.ddd : Return "450 Requested file action not taken"
      Case FileSystemAccessMessages.ddd : Return "257 ""{0}"" is current directory."


      Case Else : Return String.Empty
    End Select

  End Function

End Class
