Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Threading

Public Class RateLimitingStream
  Inherits Stream

  Private _baseStream As Stream
  Private _watch As System.Diagnostics.Stopwatch
  Private _speedLimit As Integer
  Private _transmitted As Long
  Private _resolution As Double

  Public Sub New(baseStream As Stream, speedLimit As Integer)
    Me.New(baseStream, speedLimit, 1)
  End Sub

  Public Sub New(baseStream As Stream, speedLimit As Integer, resolution As Double)

    _baseStream = baseStream
    _watch = New System.Diagnostics.Stopwatch()
    _speedLimit = speedLimit
    _resolution = resolution

  End Sub

  Public Overrides Sub Write(buffer As Byte(), offset As Integer, count As Integer)

    If (Not _watch.IsRunning) Then
      _watch.Start()
    End If

    Dim dataSent As Integer = 0

    While (_speedLimit > 0 AndAlso _transmitted >= (_speedLimit * _resolution))
      Thread.Sleep(10)

      If _watch.ElapsedMilliseconds > (1000 * _resolution) Then
        _transmitted = 0
        _watch.Restart()
      End If
    End While

    _baseStream.Write(buffer, offset, count)
    _transmitted += count
    dataSent += count

    If (_watch.ElapsedMilliseconds > (1000 * _resolution)) Then
      _transmitted = 0
      _watch.Restart()
    End If

  End Sub

  Public Overrides ReadOnly Property CanRead() As Boolean
    Get
      Return False
    End Get
  End Property

  Public Overrides ReadOnly Property CanSeek() As Boolean
    Get
      Return False
    End Get
  End Property

  Public Overrides ReadOnly Property CanWrite() As Boolean
    Get
      Return True
    End Get
  End Property

  Public Overrides Sub Flush()
    _baseStream.Flush()
  End Sub

  Public Overrides ReadOnly Property Length() As Long
    Get
      Return _baseStream.Length
    End Get
  End Property

  Public Overrides Property Position() As Long
    Get
      Return _baseStream.Position
    End Get
    Set(value As Long)
      Throw New NotImplementedException()
    End Set
  End Property

  Public Overrides Function Read(buffer As Byte(), offset As Integer, count As Integer) As Integer
    Throw New NotImplementedException()
  End Function

  Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
    Throw New NotImplementedException()
  End Function

  Public Overrides Sub SetLength(value As Long)
    Throw New NotImplementedException()
  End Sub

  Protected Overrides Sub Dispose(disposing As Boolean)
    _watch.[Stop]()

    MyBase.Dispose(disposing)
  End Sub

End Class
