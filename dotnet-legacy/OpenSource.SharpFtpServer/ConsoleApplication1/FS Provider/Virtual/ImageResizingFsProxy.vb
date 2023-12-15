'Imports System.IO
'Imports System.Drawing
'Imports System.Drawing.Imaging

'Public Class ImageResizingFsProxy
'  Inherits FsProxy

'  Private _MaximumSize As Size

'  Public Sub New(source As IFileSystemProvider, maximumSize As Size)
'    MyBase.New(source)
'    _MaximumSize = maximumSize
'  End Sub

'  Public ReadOnly Property MaximumSize As Size
'    Get
'      Return _MaximumSize
'    End Get
'  End Property

'  Public Overrides Function FileSize(pathName As String) As Long
'    Using str = Me.OpenFileForRead(pathName)
'      Return str.Length
'    End Using
'  End Function

'  Public Overrides Function OpenFileForRead(pathName As String) As Stream
'    Dim extension As String
'    Dim fs As Stream

'    extension = Path.GetExtension(pathName)
'    fs = MyBase.OpenFileForRead(pathName)

'    Select Case extension.ToLower()
'      Case ".jpg", ".jpeg" : Return OpenResizedStream(fs, _MaximumSize, ImageFormat.Jpeg)
'      Case ".bmp" : Return OpenResizedStream(fs, _MaximumSize, ImageFormat.Bmp)
'      Case ".gif" : Return OpenResizedStream(fs, _MaximumSize, ImageFormat.Gif)
'      Case ".png" : Return OpenResizedStream(fs, _MaximumSize, ImageFormat.Png)
'      Case ".tiff" : Return OpenResizedStream(fs, _MaximumSize, ImageFormat.Tiff)
'      Case Else : Return fs
'    End Select

'  End Function

'  Protected Shared Function OpenResizedStream(ByRef sourceStream As Stream, maxSize As Size, targetFormat As ImageFormat) As Stream
'    Dim targetStream As Stream = New MemoryStream
'    Dim newSize As Size

'    Using sourceImage As Image = Image.FromStream(sourceStream)

'      Using targetImage As Bitmap = New Bitmap(newSize.Width, newSize.Width)
'        Using g As Graphics = Graphics.FromImage(targetImage)

'          g.DrawImage(sourceImage, 0, 0, newSize.Width, newSize.Height)

'          targetImage.Save(targetStream, targetFormat)
'          targetStream.Flush()

'        End Using
'      End Using

'    End Using

'    sourceStream.Close()
'    sourceStream.Dispose()
'    sourceStream = Nothing

'    Return targetStream
'  End Function

'  Public Overrides Function OpenFileForWrite(pathName As String) As Stream
'    Throw New Exception("Editing resized images is forbidden!")
'  End Function

'  Public Overrides Function OpenFileForAppend(pathName As String) As Stream
'    Throw New Exception("Editing resized images is forbidden!")
'  End Function

'End Class
