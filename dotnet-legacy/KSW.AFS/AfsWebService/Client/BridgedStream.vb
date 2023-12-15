Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices


'TODO: mit diesen stream muss ein fragmentierter upload und download erreicht werden

'insbesondere der upload muss 

Public Class BridgedStream
  Inherits Stream

  'TODO: KOMMUNIKATION MUSS NOCH IN CRYPTOSTREAM GEWRAPPT WERDEN

#Region " Declarations & Constructor "

  Private _Client As WebbridgeClient
  Private _IsUpstream As Boolean
  Private _StreamId As Guid

  Private _CanSeek As Boolean
  Private _CanRead As Boolean
  Private _CanWrite As Boolean
  Private _Length As Long = 0
  Private _Position As Long = 0

  Public Sub New(client As WebbridgeClient, isUpstream As Boolean, streamId As Guid)
    MyBase.New()
    _Client = client
    _IsUpstream = isUpstream
    _StreamId = streamId
    Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("GetStreamState", _StreamId))
  End Sub

#End Region

#Region " Properties "

  Public Overrides ReadOnly Property CanRead As Boolean
    Get
      Return _CanRead
    End Get
  End Property

  Public Overrides ReadOnly Property CanSeek As Boolean
    Get
      Return _CanSeek
    End Get
  End Property

  Public Overrides ReadOnly Property CanWrite As Boolean
    Get
      Return _CanWrite
    End Get
  End Property

  Public Overrides ReadOnly Property Length As Long
    Get
      Return _Length
    End Get
  End Property

  Public Overrides Property Position As Long
    Get
      Return _Position
    End Get
    Set(value As Long)
      Me.SetPosition(value)
    End Set
  End Property

#End Region

#Region " Methods "

  Private Sub SetPosition(value As Long)
    Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("SetStreamPosition", _StreamId, value))
  End Sub

  Public Overrides Sub SetLength(value As Long)
    Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("SetStreamLength", _StreamId, value))
  End Sub

  Public Overrides Sub Flush()
    Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("FlushStream", _StreamId))
  End Sub

  Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
    If (offset = 0 AndAlso count = buffer.Length) Then
      Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("WriteStream", _StreamId, buffer))
    Else
      Dim uploadBuffer(count - 1) As Byte
      For i As Integer = 0 To count - 1
        uploadBuffer(i) = buffer(i + offset)
      Next
      Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("WriteStream", _StreamId, uploadBuffer))
    End If
  End Sub

  Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
    Dim result = _Client.ExecuteRequest(Of StreamReadContract)("ReadStream", _StreamId, count)
    result.Data.CopyTo(buffer, offset)
    Me.UpdateState(result)
    Return result.Result
  End Function

  Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
    Me.UpdateState(_Client.ExecuteRequest(Of StreamStateInfo)("SeekStream", _StreamId, offset, origin))
    Return _Position
  End Function

  Public Overrides Sub Close()
    MyBase.Close()
    _Client.ExecuteRequest(Of StreamStateInfo)("CloseStream", _StreamId)
  End Sub

  Private Sub UpdateState(info As StreamStateInfo)
    _Length = info.Length
    _Position = info.Position
    _CanSeek = info.CanSeek
    _CanRead = info.CanRead
    _CanWrite = info.CanWrite
  End Sub

#End Region

End Class