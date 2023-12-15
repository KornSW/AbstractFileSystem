Imports System
Imports System.ComponentModel
Imports System.IO.AbstractFilesystem
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.AbstractFilesystem

Public Class FormMain

  'this is only a helper for manageing
  'the current state of the edit usecase
  '(pending changes / filename / dialog flow )
  Private _Fsm As FileStateManager

  Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    _Fsm = New FileStateManager

    _Fsm.CurrentFileNameDisplayHandler = Sub(f) Me.UpdateHeadLine()
    _Fsm.PendingChangesDisplayHandler = Sub(c) Me.UpdateHeadLine()
    _Fsm.ResetToNewContentHandler = AddressOf Me.ResetContent
    _Fsm.OpenFileHandler = AddressOf Me.ReadContentFromFile
    _Fsm.SaveFileHandler = AddressOf Me.WriteContentToFile

    '#########################################################
    'this is where we wire up to AFS specific methods
    'and get away from the default 'Windows' style
    'filesystem access [COMMENT THIS OUT TO HAVE THE OLD STYLE]

    '_Fsm.SelectSourceFileNameHandler = AddressOf Me.SelectSourceAfsFileName
    '_Fsm.SelectTargetFileNameHandler = AddressOf Me.SelectTargetAfsFileName
    _Fsm.OpenFileHandler = AddressOf Me.ReadContentFromAfsFile
    _Fsm.SaveFileHandler = AddressOf Me.WriteContentToAfsFile


    'temporays reference to afscommonproviders to do this...
    AfsEnvironment.DefaultProviderFactory = New LocalDriveFactory()


    '#########################################################

  End Sub

#Region " AFS Integration "

  Private Function SelectTargetAfsFileName() As String
    Using dlg As New FileBrowserDialog
      dlg.Mode = FileDialogMode.SaveFile

      If (_Fsm.CurrentFileName IsNot Nothing) Then
        dlg.FileName = IO.Path.GetFileName(_Fsm.CurrentFileName)
        dlg.InitialDirectory = IO.Path.GetDirectoryName(_Fsm.CurrentFileName)
      End If
      dlg.Title = "Datei Speichern"
      dlg.Filter = _Fsm.DefaultFileNamePattern

      Select Case dlg.ShowDialog
        Case DialogResult.Cancel, DialogResult.Abort
          Return Nothing
        Case Else
          Return dlg.FileName
      End Select

    End Using
  End Function

  Private Function SelectSourceAfsFileName() As String
    Using dlg As New FileBrowserDialog
      dlg.Mode = FileDialogMode.OpenFile

      dlg.Title = "Datei Öffnen"
      dlg.Filter = _Fsm.DefaultFileNamePattern

      Select Case dlg.ShowDialog
        Case DialogResult.Cancel, DialogResult.Abort
          Return Nothing
        Case Else
          Return dlg.FileName
      End Select

    End Using
  End Function

  Private Function ReadContentFromAfsFile(fileName As String) As Boolean
    Try
      Me.gTxtContent.Text = AfsFile.ReadAllText(fileName, Encoding.Default)
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Fehler beim laden", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

  Private Function WriteContentToAfsFile(fileName As String) As Boolean
    Try
      AfsFile.WriteAllText(fileName, Me.gTxtContent.Text, Encoding.Default)
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Fehler beim speichern", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

#End Region

#Region " File & Content Operations "

  Private Function ReadContentFromFile(fileName As String) As Boolean
    Try
      Me.gTxtContent.Text = IO.File.ReadAllText(fileName, Encoding.Default)
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Fehler beim laden", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

  Private Function WriteContentToFile(fileName As String) As Boolean
    Try
      IO.File.WriteAllText(fileName, Me.gTxtContent.Text, Encoding.Default)
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Fehler beim speichern", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

  Private Sub ResetContent()
    Me.gTxtContent.Text = String.Empty
  End Sub

#End Region

#Region " Event Subscriber "

  Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    If (Not _Fsm.SaveChangesIfRequired) Then
      e.Cancel = True
    End If
  End Sub

  Private Sub gTxtContent_TextChanged(sender As Object, e As EventArgs) Handles gTxtContent.TextChanged
    _Fsm.PendingChanges = True
  End Sub

  Private Sub gBtnNew_Click(sender As Object, e As EventArgs) Handles gBtnNew.Click
    _Fsm.NewFile()
  End Sub

  Private Sub gBtnOpen_Click(sender As Object, e As EventArgs) Handles gBtnOpen.Click
    _Fsm.OpenFile()
  End Sub

  Private Sub gBtnSave_Click(sender As Object, e As EventArgs) Handles gBtnSave.Click
    _Fsm.SaveFile()
  End Sub

  Private Sub gBtnSaveAs_Click(sender As Object, e As EventArgs) Handles gBtnSaveAs.Click
    _Fsm.SaveFileAs()
  End Sub

#End Region

#Region " Misc "

  Private Sub UpdateHeadLine()

    If (_Fsm.CurrentFileName Is Nothing) Then
      Me.Text = "AFS Notepad"
    Else
      Me.Text = "AFS Notepad - " & IO.Path.GetFileName(_Fsm.CurrentFileName)
    End If
    If (_Fsm.PendingChanges) Then
      Me.Text = "*" & Me.Text
    End If

  End Sub

#End Region

End Class
