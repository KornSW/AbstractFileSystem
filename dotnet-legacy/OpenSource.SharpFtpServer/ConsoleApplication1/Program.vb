Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Drawing

Public Module Program

  Public Const CatchblocksEnabled As Boolean = False

  Private Const CacheDir As String = "C:\Temp\FTP\Cache"
  Private Const RootDir As String = "C:\Temp\FTP\Root"

  Public Function Main(args() As String) As Integer

    Directory.CreateDirectory("")

    'Using server As New FtpServer(IPAddress.IPv6Any, 21)
    'Dim listenIp As IPAddress = IPAddress.Loopback
    'Dim listenIp As IPAddress = IPAddress.Parse("10.20.20.20")
    Dim listenIp As IPAddress = IPAddress.Parse("127.0.0.1")

    ' 127.0.0.1 hns test \

    Using server As New FtpServer(New UserManager(), AddressOf CreateLocalFsProvider, listenIp, 21)
      server.Start()
      Console.WriteLine("Press any key to stop...")
      Console.ReadKey(True)
    End Using

    Return 0
  End Function

  Private Function CreateLocalFsProvider() As IFileSystemProvider
    Dim originalFs = New LocalFsProvider(RootDir)
    Return originalFs
  End Function

  Private Function CreateResizingImgProvider() As IFileSystemProvider
    'Dim originalFs = New LocalFsProvider(RootDir)
    'Dim resizedImgFs = New ImageResizingFsProxy(originalFs, New Size(800, 800))
    'Dim cachedFs = New CachingFsProxy(resizedImgFs, New DirectoryInfo(CacheDir))
    'Return cachedFs
  End Function

End Module
