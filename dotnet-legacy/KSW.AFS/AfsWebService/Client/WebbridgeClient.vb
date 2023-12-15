Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.AbstractFilesystem
Imports System.Linq
Imports System.Security
Imports System.Security.Cryptography
Imports System.Windows.Markup
Imports System.Xml
Imports KSW.Afs.WebBridge
Imports KSW.Base

Public Class WebbridgeClient
  Implements IAfsSource
#Region "..."

  Private _Url As String
  Private _Wc As New ExtendedWebClient()
  Private _Secret As New SecureString
  Private _UserName As String = Nothing
  Private _Password As SecureString = Nothing
  Private _LoggedOn As Boolean = False

  Public Sub New(url As String)
    Me.New(url, My.Settings.SharedSecret)
  End Sub

  Public Sub New(url As String, sharedSecret As String)
    _Url = url
    _Wc.IgnoreSslCertificateErrors = True
    _Secret.AppendString(sharedSecret)
  End Sub

  Public Sub New(url As String, userName As String, password As SecureString)
    Me.New(url, My.Settings.SharedSecret, userName, password)
  End Sub

  Public Sub New(url As String, sharedSecret As String, userName As String, password As SecureString)
    _Url = url
    _Wc.IgnoreSslCertificateErrors = True
    _Secret.AppendString(sharedSecret)
    _UserName = userName
    _Password = password
  End Sub

  Protected Friend Function ExecuteRequest(Of TResult)(ParamArray args() As Object) As TResult
    If (_LoggedOn = False AndAlso _Password IsNot Nothing AndAlso _UserName IsNot Nothing) Then
      Try
        _LoggedOn = True
        Logon(_UserName, _Password)
      Catch ex As Exception
        _LoggedOn = False
        Throw
      End Try
    End If

    Dim hash As String = Guid.NewGuid().ToString().MD5()
    Dim oneTimeKey As New SecureString
    oneTimeKey.AppendString((_Secret.GetMD5Hash() & hash & "!").MD5())
    Dim wrp As New ObjectTransportWrapper(oneTimeKey)
    Dim aLittleBitOfFake = "AccessTarget=EwsManagedApi&Class=CalendarServices&Request=SyncItem&Hash=" & hash & "&CompressedItemData="

    Dim result As Object

    'If (GetType(TResult) = GetType(IO.Stream)) Then
    '  Dim ret As New MemoryStream
    '  Dim requestData As New MemoryStream
    '  Dim writer As New StreamWriter(requestData)
    '  writer.Write(aLittleBitOfFake & wrp.WrapToString(args))
    '  requestData.Position = 0
    '  _Wc.UploadData(_Url, requestData, ret)
    '  result = ret
    'Else
    Dim resultData = _Wc.UploadString(_Url, aLittleBitOfFake & wrp.WrapToString(args))
    result = wrp.UnWrapFromString(Of Object)(resultData)
    If (TypeOf (result) Is Exception) Then
      Dim ex = DirectCast(result, Exception)
      Throw New ApplicationException("Exception on Server: " & ex.Message, ex)
    End If
    'End If

    Return DirectCast(result, TResult)
  End Function

  Private Sub Logon(userName As String, password As SecureString)
    Me.ExecuteRequest(Of Object)("Logon", userName, password.GetEncryptedString(_Secret))
  End Sub

  Private Sub Logoff()
    Me.ExecuteRequest(Of Object)("Logoff")
  End Sub

#End Region

  Public Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    'Return Me.ExecuteRequest(Of String())("GetDirectories")
  End Function

  Public Function GetDirectories(relativePath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories

  End Function

  Public Function GetDirectory(relativePath As String) As AfsDirectoryInfo Implements IAfsDirectoryInfo.GetDirectory

  End Function







  Public Function GetFile(relativePath As String) As AfsFileInfo Implements IAfsDirectoryInfo.GetFile

  End Function

  Public Function GetFiles() As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles

  End Function

  Public Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles

  End Function

  Public Function GetSource() As IAfsSource Implements IAfsDirectoryInfo.GetSource

  End Function



  Public Function GetFileCreationTime(relativePath As String) As Date Implements IAfsSource.GetFileCreationTime

  End Function

  Public Function GetFileLastWriteTime(relativePath As String) As Date Implements IAfsSource.GetFileLastWriteTime

  End Function

  Public Function GetFileLength(relativePath As String) As Long Implements IAfsSource.GetFileLength

  End Function

  Public Function OpenRead(relativePath As String) As Stream Implements IAfsSource.OpenRead
    Return New BridgedStream(Me, False, Me.ExecuteRequest(Of Guid)("OpenRead", relativePath))
  End Function

  Public Function OpenWrite(relativePath As String) As Stream Implements IAfsSource.OpenWrite
    Return New BridgedStream(Me, True, Me.ExecuteRequest(Of Guid)("OpenWrite", relativePath))
  End Function

  Public Function SearchFiles(startDirectory As String, mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsSource.SearchFiles

  End Function
  Public Function SearchFiles(mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.SearchFiles

  End Function

  Public ReadOnly Property SourceIdentifier As String Implements IAfsSource.SourceIdentifier
    Get
      Return _Url.MD5()
    End Get
  End Property

  Public Function TestAccessability(relativePath As String) As Boolean Implements IAfsSource.TestAccessability

  End Function

End Class
