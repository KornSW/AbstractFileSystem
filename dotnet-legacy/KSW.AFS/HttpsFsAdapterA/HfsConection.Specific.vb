Imports System
Imports System.Net
Imports System.Security
Imports KSW.Base
Imports System.Xml
Imports System.Text
Imports AfsKernel

Public Class HfsConection

  Private _Http As HttpSession = Nothing

  Private Const _LogonUrl As String = "~login"

  Public Sub New()
  End Sub

  Public Sub New(url As String)
    Me.Url = url
  End Sub

  Private _Url As String = ""
  Public Property Url As String
    Get
      Return _Url
    End Get
    Set(value As String)
      If (Not _Url = value) Then
        _Url = value
        _AuthenticatedAs = Nothing
        _Http = New HttpSession

        If (_Url.Length > 0 AndAlso Not _Url.EndsWith("/")) Then
          _Url = _Url & "/"
        End If
        _RootFolder = New HfsFolder(Me, _Url)
        _RootFolder.Reload()
      End If
    End Set
  End Property

  Public Function Authenticate(userName As String, password As SecureString) As Boolean
    Try
      Dim requestUrl As String = _Url & _LogonUrl
      _Http.ConfigureForBasicAuth(requestUrl, New NetworkCredential(userName, password))
      Dim response As String = _Http.HttpGetString(requestUrl)
      _RootFolder.Reload()
      _AuthenticatedAs = userName
      Return (Not response.Contains("ANMELDEN"))
    Catch
      Return False
    End Try
  End Function

  Public Sub Test2()
    Dim resultXml As String = ""
    resultXml = _Http.HttpGetString("http://file.familiekorn.net/")
    resultXml = resultXml.ExtractRegions("<!--{", "}-->")
    resultXml.ParseXml()


  End Sub
  Public Function Test() As Image
    ' _Http.HttpGetFile("http://file.familiekorn.net/FolderImages/folder.png", "C:\test.png")

    Return _Http.HttpGetImage("http://file.familiekorn.net/FolderImages/folder.png")



  End Function

  Public Function Authenticate(userName As String, password As String) As Boolean
    Try
      Dim requestUrl As String = _Url & _LogonUrl
      _Http.ConfigureForBasicAuth(requestUrl, New NetworkCredential(userName, password))
      Dim response As String = _Http.HttpGetString(requestUrl)
      _RootFolder.Reload()
      _AuthenticatedAs = userName
      Return (Not response.Contains("ANMELDEN"))
    Catch
      Return False
    End Try
  End Function

  Public Property TransmissionDiag As Action(Of String, String) = Nothing
  Private Sub Log(url As String, output As String)
    If (Me.TransmissionDiag IsNot Nothing) Then
      Me.TransmissionDiag.Invoke(url, output)
    End If
  End Sub

  Private _AuthenticatedAs As String = Nothing

  Public ReadOnly Property AuthenticatedAs As String
    Get
      Return _AuthenticatedAs
    End Get
  End Property

  Private _RootFolder As New HfsFolder(Me, String.Empty)

  Public ReadOnly Property RootFolder As HfsFolder
    Get
      Return _RootFolder
    End Get
  End Property

  Public Class HfsFolder

    Public Sub New(connection As HfsConection, folderUrl As String)

    End Sub

    Public Sub Reload()
      'Dim requestUrl As String = _Url & _LogonUrl
      'Dim resonse As String = _WebClient.DownloadString(requestUrl)
      'Me.Log(requestUrl, resonse)
    End Sub

    Public ReadOnly Property FolderName As String
      Get

      End Get
    End Property

    Public ReadOnly Property FolderImage As Image
      Get

      End Get
    End Property

    Public ReadOnly Property RelativePath As String
      Get

      End Get
    End Property


    Public ReadOnly Property FolderUrl As String
      Get

      End Get
    End Property

    Public ReadOnly Property SubFolders As IEnumerable(Of HfsFolder)
      Get

      End Get
    End Property

    Public ReadOnly Property Files As IEnumerable(Of HfsFile)
      Get

      End Get
    End Property

    Public ReadOnly Property CanUpload As Boolean
      Get

      End Get
    End Property

    Public Sub UploadFile(localFileName As String)



    End Sub

  End Class

  Public Class HfsFile

    Public Sub New(folder As HfsFolder)

    End Sub

    Public ReadOnly Property Folder As HfsFolder
      Get

      End Get
    End Property

    Public ReadOnly Property RelativePath As String
      Get

      End Get
    End Property


    Public ReadOnly Property FileName As String
      Get

      End Get
    End Property

    Public ReadOnly Property FileTitle As String
      Get

      End Get
    End Property

    Public ReadOnly Property FileExtension As String
      Get

      End Get
    End Property

    Public ReadOnly Property FileImage As Image
      Get

      End Get
    End Property

    Public ReadOnly Property FileUrl As String
      Get

      End Get
    End Property

    Public ReadOnly Property SizeBytes As Decimal
      Get

      End Get
    End Property

    Public ReadOnly Property SizeUnit As String
      Get

      End Get
    End Property

    Public ReadOnly Property ChangeDate As DateTime
      Get

      End Get
    End Property

    Public Sub Download(localFileName As String)

    End Sub

    Public Sub Download(localFileName As String, progressInfoHandler As Action(Of Long, Long))

    End Sub


  End Class

End Class

Public Class CommentEncapsolatedXmlParser
  Implements IDisposable

  Public Sub New()
  End Sub

  Public Sub Parse(rawText As String)

    Dim extractedRawContent As New StringBuilder

    Dim currentPosition As Integer = 0




















  End Sub

  Public Property CommentBeginPhrase As String = "<!--"
  Public Property CommentEndPhrase As String = "-->"

  Private _ExtractedXml As XmlDocument = New XmlDocument()

  Public ReadOnly Property ExtractedXml As XmlDocument
    Get
      Return _ExtractedXml
    End Get
  End Property

#Region " IDisposable "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _AlreadyDisposed As Boolean = False

  ''' <summary>
  ''' Dispose the current object instance
  ''' </summary>
  Protected Overridable Sub Dispose(disposing As Boolean)
    If (Not _AlreadyDisposed) Then
      If (disposing) Then
        _ExtractedXml.TryDispose()
      End If
      _AlreadyDisposed = True
    End If
  End Sub

  ''' <summary>
  ''' Dispose the current object instance and suppress the finalizer
  ''' </summary>
  Public Sub Dispose() Implements IDisposable.Dispose
    Me.Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub

#End Region

End Class
