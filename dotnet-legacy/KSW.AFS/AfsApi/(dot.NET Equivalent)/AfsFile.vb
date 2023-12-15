Imports System.Text

Public Class AfsFile

  Private Sub New()
  End Sub

#Region " ReadAlltext "

  Public Shared Function ReadAlltext(fullyQualifiedAfsPath As String) As String
    Dim uri As New AfsUri(fullyQualifiedAfsPath)
    Return ReadAlltext(uri)
  End Function

  Public Shared Function ReadAlltext(fullyQualifiedAfsPath As String, encoding As Encoding) As String
    Dim uri As New AfsUri(fullyQualifiedAfsPath)
    Return ReadAlltext(uri, encoding)
  End Function

  Public Shared Function ReadAlltext(afsUri As AfsUri) As String
    Return ReadAlltext(afsUri, Encoding.UTF8)
  End Function

  Public Shared Function ReadAlltext(afsUri As AfsUri, encoding As Encoding) As String
    Dim src As IAfsSource
    Dim file As AfsFileInfo
    src = AfsEnvironment.FindSourceForUri(afsUri)
    file = src.GetFile(afsUri.RelativePath)
    Using stream = file.OpenRead()
      Using reader As New StreamReader(stream, encoding)
        Return reader.ReadToEnd()
      End Using
    End Using
  End Function

#End Region

#Region " WriteAllText "

  Public Shared Sub WriteAllText(fullyQualifiedAfsPath As String, contents As String)
    Dim uri As New AfsUri(fullyQualifiedAfsPath)
    WriteAllText(uri, contents)
  End Sub

  Public Shared Sub WriteAllText(fullyQualifiedAfsPath As String, contents As String, encoding As Encoding)
    Dim uri As New AfsUri(fullyQualifiedAfsPath)
    WriteAllText(uri, contents, encoding)
  End Sub

  Public Shared Sub WriteAllText(afsUri As AfsUri, contents As String)
    WriteAllText(afsUri, contents, Encoding.UTF8)
  End Sub

  Public Shared Sub WriteAllText(afsUri As AfsUri, contents As String, encoding As Encoding)
    Dim src As IAfsSource
    Dim file As AfsFileInfo
    src = AfsEnvironment.FindSourceForUri(afsUri)
    file = src.GetFile(afsUri.RelativePath)
    Using stream = file.OpenWrite()
      Using writer As New StreamWriter(stream, encoding)
        writer.Write(contents)
        writer.Flush()
      End Using
    End Using
  End Sub

#End Region

End Class
