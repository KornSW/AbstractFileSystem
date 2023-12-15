Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.IO.AbstractFilesystem
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security
Imports System.Security.AccessControl
Imports System.Security.Cryptography
Imports System.Security.Principal
Imports System.Text
Imports System.Web
Imports KSW.Afs
Imports KSW.Base
Imports KSW.Base.Shell.Web
Imports KSW.Base.Ui.Web

Public Class BridgedAfsSession
  Implements IDisposable

#Region " Init (virtual Tree)"

  Private _Root As New AfsVirtualDirectory

  Public Function InitializeUserProfile(userName As String) As Boolean

    Me.CloseAllStreams()
    _Root.UnmnountAll()

    If (userName Is Nothing) Then
      Return True
    End If

    Dim cfg = AppConfig.Load(Of AfsServerConfig)("AfsServerConfig.xml")
    If (cfg Is Nothing) Then
      cfg = New AfsServerConfig
    End If

    Dim logon As AfsLogonUserConfig = (From l In cfg.Logons Where l.UserName.ToLower() = userName.ToLower()).FirstOrDefault()

    If (logon Is Nothing) Then
      Return False
    End If

    _Root.MountByConfig(logon.RootDir)
    For Each groupName In logon.AttachedGroups
      Dim group As AfsGroupConfig = (From l In cfg.Groups Where l.Name.ToLower() = groupName.ToLower()).FirstOrDefault()
      If (group IsNot Nothing) Then
        _Root.MountByConfig(group.RootDir)
      End If
    Next

    Return True
  End Function

#End Region

#Region " Bridged Streams "
#Region "..."

  Private _Streams As New Dictionary(Of Guid, Stream)

  Protected Function GetStream(id As Guid) As Stream
    Return _Streams(id)
  End Function

  Public Function GetStreamState(id As Guid) As StreamStateInfo
    Return Me.GetStreamState(Me.GetStream(id))
  End Function

  Protected Function GetStreamState(str As Stream) As StreamStateInfo
    Return New StreamStateInfo With
           {.Length = str.Length,
            .Position = str.Position,
            .CanRead = str.CanRead,
            .CanSeek = str.CanSeek,
            .CanWrite = str.CanWrite}
  End Function

  Public Sub CloseAllStreams()
    For Each id In _Streams.Keys.ToArray()
      Me.CloseStream(id)
    Next
  End Sub

  Public Sub CloseStream(id As Guid)
    If (_Streams.ContainsKey(id)) Then
      _Streams(id).Dispose()
      _Streams.Remove(id)
    End If
  End Sub

#End Region

  Public Function OpenRead(relativePath As String) As Guid
    Dim id As Guid = New Guid
    _Streams.Add(id, _Root.OpenRead(relativePath))
    Return id
  End Function

  Public Function OpenWrite(relativePath As String) As Guid
    Dim id As Guid = New Guid
    _Streams.Add(id, _Root.OpenWrite(relativePath))
    Return id
  End Function

  Public Function SeekStream(id As Guid, offset As Long, origin As SeekOrigin) As StreamStateInfo
    Dim str = Me.GetStream(id)
    str.Seek(offset, origin)
    Return Me.GetStreamState(str)
  End Function

  Public Function ReadStream(id As Guid, count As Integer) As StreamReadContract
    Dim str = Me.GetStream(id)
    Dim buffer(count - 1) As Byte
    Dim result = New StreamReadContract With
           {.Length = str.Length,
            .Position = str.Position,
            .CanRead = str.CanRead,
            .CanSeek = str.CanSeek,
            .CanWrite = str.CanWrite}
    result.Result = str.Read(buffer, 0, count)
    result.Data = buffer
    Return result
  End Function

  Public Function WriteStream(id As Guid, buffer() As Byte) As StreamStateInfo
    Dim str = Me.GetStream(id)
    str.Write(buffer, 0, buffer.Length)
    Return Me.GetStreamState(str)
  End Function

  Public Function FlushStream(id As Guid) As StreamStateInfo
    Dim str = Me.GetStream(id)
    str.Flush()
    Return Me.GetStreamState(str)
  End Function

  Public Function SetStreamPosition(id As Guid, value As Long) As StreamStateInfo
    Dim str = Me.GetStream(id)
    str.Position = value
    Return Me.GetStreamState(str)
  End Function

  Public Function SetStreamLength(id As Guid, value As Long) As StreamStateInfo
    Dim str = Me.GetStream(id)
    str.SetLength(value)
    Return Me.GetStreamState(str)
  End Function

#End Region

#Region " Browsing "

  Public Function SearchFiles(startDirectory As String, mask As String, recursive As Boolean, limitResults As Integer) As FileContract()
    _Root.GetDirectory(startDirectory).SearchFiles(mask, recursive, limitResults).Select(Function(dir) New FileContract(dir)).ToArray()
  End Function

  Public Function ListFiles(parentDirectory As String) As FileContract()
    Return _Root.GetDirectory(parentDirectory).GetFiles().Select(Function(dir) New FileContract(dir)).ToArray()
  End Function

  Public Function GetFile(filePath As String) As FileContract



  End Function

  Public Function ListDirectories(parentDirectory As String) As DirectoryContract()
    Return _Root.GetDirectory(parentDirectory).GetDirectories().Select(Function(dir) New DirectoryContract(dir)).ToArray()
  End Function

  Public Sub DeleteFile(parentDirectory As String, fileName As String)
    ' Return _Root.GetDirectory(parentDirectory).GetFile(fileName).Delete
  End Sub

  Public Sub DeleteDirectory(directory As String)
    'Dim dir = _Root.GetDirectory(directory)
    'If (dir.IsEmpty) Then
    '  Throw New Exception("Directory is not empty")
    'End If
    '_Root.GetDirectory(directory).Delete()
  End Sub

  Public Sub UploadFile(parentDirectory As String, fileName As String, content As Byte())

  End Sub

  Public Function DownloadFile(parentDirectory As String, fileName As String) As Byte()

  End Function

  Public Function GetThumbnail(parentDirectory As String, fileName As String, size As Integer) As Image

  End Function

#End Region

#Region " IDisposable "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _AlreadyDisposed As Boolean = False

  ''' <summary>
  ''' Dispose the current object instance
  ''' </summary>
  Protected Overridable Sub Dispose(disposing As Boolean)
    If (Not _AlreadyDisposed) Then
      If (disposing) Then
        Me.CloseAllStreams()
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
